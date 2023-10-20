
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] private byte damage;
    private Rigidbody2D rb;
    private SpriteRenderer dir;
    private Color Original;
    private Vector3 sightdirect;

    public GameObject[] wp;    
    public Transform player;
    public Animator anim;   
    public float speed = 3f;
    public sbyte currentIndex = 0;
    public bool freeze, ischasing, chased, isdead;


    EnemyBaseState CurrentState;
    public Dead DeadState = new Dead();
    public Patrol PatrolState = new Patrol();
    public Idle IdleState = new Idle();
    public Chase ChaseState = new Chase();
    public ReturnPatrol ReturnState = new ReturnPatrol();


    RaycastHit2D hit;
    private void Awake()
    {
        anim.GetComponent<Animator>();
        Original = GetComponent<Renderer>().material.color;
    }
    void Start()
    {
        CurrentState = PatrolState;
        CurrentState.EnterState(this);
    }
    void Update() 
    {
        sightdirect = transform.localScale * new Vector2(1,0);
        hit = Physics2D.Raycast(transform.position, sightdirect, 10f, ~(1 << 8));
        Debug.Log(hit.collider);
        Debug.DrawRay(transform.position, sightdirect * 10f, Color.red);
        if (!isdead)
        {
            CurrentState.UpdateState(this);
        }
        else
        {
            CurrentState = DeadState;
            CurrentState.EnterState(this);
            CurrentState.UpdateState(this);
        }
    }
    public void SwitchState(EnemyBaseState state)
    {
        CurrentState = state;
        state.EnterState(this);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(collision.gameObject.tag == "immortal")
        { 
            isdead = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Physics2D.IgnoreLayerCollision(7, 8);
            return ;
        }
        Vector2 collisionNormal = collision.contacts[0].normal;      
        dir = collision.gameObject.GetComponent<SpriteRenderer>();
        rb = collision.gameObject.GetComponent<Rigidbody2D>();  
        if (collision.gameObject.name == "player")
        {
            if (DotTest(collisionNormal))
            {
                isdead = true;
                rb.velocity = Vector2.zero;
                collision.gameObject.GetComponent<playerMovement>().SetDoubleJump(true);
                if (!dir.flipX) rb.AddForce(new Vector2(400f, 400f));
                else rb.AddForce(new Vector2(-400f, 400f));
            }         
            else
                collision.gameObject.GetComponent<Life>().Damaged(damage);
        }
    }
    public bool Chasezone()
    {
        if (hit.collider != null)
            return Physics2D.Raycast(transform.position, sightdirect, 10f, ~(1 << 8)).collider.name == "player";
        else
            return false;
    }

    private bool DotTest(Vector2 collisionNor)
    {
        return Vector2.Dot(collisionNor, Vector2.down) > 0.25f;
    } 
    public void SetDead()
    {
        GetComponent<Renderer>().material.color = Original;
        anim.SetTrigger("explode");
    }
    private void Explode()
    {
        GetComponent<ItemBag>().InstantiateItem(transform.position);
        Destroy(gameObject);
    }
    public bool OutRange(Transform a , GameObject[] b)
    {
        return (a.position.x < b[0].transform.position.x || a.position.x > b[1].transform.position.x);
    }
    public void ReturnPatrol()
    {
        if (!freeze)
        {
            anim.SetInteger("state", 1);
            Vector3 direction = (wp[currentIndex].transform.position - transform.position).normalized;
            if (direction.x > 0)
                transform.localScale = Vector2.one;
            else if (direction.x < 0)
                transform.localScale = new Vector2(-1,1);
            chased = (Vector2.Distance(transform.position, wp[0].transform.position) < .1f || Vector2.Distance(transform.position, wp[1].transform.position) < .1f) ? false : true;
            transform.position = Vector2.MoveTowards(transform.position, wp[currentIndex].transform.position, Time.deltaTime * speed);
        }

    }
    public void Chasing()
    {
        GetComponent<Renderer>().material.color = new Color(217 / 255f, 71 / 255f, 59 / 255f);
        if (transform.position.x < player.position.x)
            transform.localScale = Vector2.one;
        else if (transform.position.x > player.position.x)
            transform.localScale = new Vector2(-1,1);
        anim.SetInteger("state", 1);
        Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed *1.8f* Time.deltaTime);
    }
    public void Patrolling()
    {
        if (Vector2.Distance(wp[currentIndex].transform.position, transform.position) < .1f)
        {
            SwitchState(IdleState);
            currentIndex++;
            if (currentIndex >= wp.Length)
                currentIndex = 0;
        }
        if (!freeze)
        {
            anim.SetInteger("state", 1);
            transform.position = Vector2.MoveTowards(transform.position, wp[currentIndex].transform.position, Time.deltaTime * speed);
        }
    }
    public void IdleCoroutine()
    {
        StartCoroutine("idle");
    }
    private void Fliping()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
    IEnumerator idle()
    {
        GetComponent<Renderer>().material.color = Original;
        float CurTime = 0;
        freeze = true;
        anim.SetInteger("state", 0);
        InvokeRepeating("Fliping", 1, 1);
        while (CurTime < 2.9f)
        {
            if (ischasing)
            {
                Debug.Log("Break");
                //transform.localScale = new Vector2(-1, transform.localScale.y);
                CancelInvoke("Fliping");
                freeze = false;
                yield break;
            }
            CurTime += Time.deltaTime;
            yield return null;
        }
        CancelInvoke("Fliping");
        if (currentIndex == 1)
            transform.localScale = Vector2.one;
        else if (currentIndex == 0)
            transform.localScale = new Vector2(-1, 1);
        freeze = false;
        yield break;
    }
}
