using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer dir;
    private BoxCollider2D coll;
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;
    [SerializeField] private LayerMask jumpcheck;
    [SerializeField] private AudioSource jump_sound;
    private float dirX;
    public bool doublejump, takedamage;
    private enum MovementStates { idle, run, jump, fall };
    private MovementStates state;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dir = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        dirX = Input.GetAxisRaw("Horizontal");
        if (!takedamage) 
            rb.velocity = new Vector2 (dirX * speed, rb.velocity.y);
        if(Grounded()) 
            doublejump = true;
        if (Input.GetButton("Jump"))
        {   
            if(Grounded()) Jumping();
            else
            {
                if (Input.GetButtonDown("Jump") && doublejump)
                {
                    anim.SetBool("double_jump",true);
                    Jumping();
                    doublejump = !doublejump;
                }
            }
        }
        UpdateAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") && gameObject.tag =="Untagged")  
            StartCoroutine("KnockBack");
    }
    IEnumerator KnockBack()
    {
        takedamage = true;
        rb.velocity = Vector2.zero;
        if (!dir.flipX) rb.AddForce(new Vector2(-200f, 200f));
        else rb.AddForce(new Vector2(200f, 200f));
        yield return new WaitForSeconds(0.5f);
        takedamage = false;
    }
    private void UpdateAnimation()
    {
        switch (dirX)
        {
            case > 0:
                state = MovementStates.run;
                dir.flipX = false;
                break;
            case < 0:
                state = MovementStates.run;
                dir.flipX = true;
                break;
            default:
                state = MovementStates.idle;
                break;
        }
        if (!Grounded()) {
            switch (rb.velocity.y)
            {
                case > .1f:
                    state = MovementStates.jump;
                    break;
                case < -.1f:
                    state = MovementStates.fall;
                    anim.SetBool("double_jump", false);
                    break;
            }
        }
        anim.SetInteger("State", (int)state);
    }
    private void Jumping()
    {
        //jump_sound.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
    }
    public bool Grounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpcheck);
    }

    public void SetDoubleJump(bool x)
    {
        doublejump = x;
    }
    public void SetSpeed(float x)
    {
        speed = x;
    }
    public void SetJumpforce(float x)
    {
        jumpforce = x;
    }
}
