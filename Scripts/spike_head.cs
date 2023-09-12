using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike_head : MonoBehaviour
{
    [SerializeField] private GameObject[] wp;
    [SerializeField] private LayerMask[] Layers;
    [SerializeField] private BoxCollider2D player;
    private Animator anim;
    private BoxCollider2D coll;
    private Vector2 vt, temp, temp1, TargetPos, size, rc;
    private float speed = 0, x1,x2, y1,y2, unit;
    private sbyte currentIndex = 1;
    private bool freeze, cons=true, p_h;
    private enum MovementStates { idle, up, down, right,left, blink };
    private MovementStates state, nextstate;

    delegate void PersonalFunction();
    PersonalFunction f;

    private void Start()
    {
        unit = player.size.x;

        informchange();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        vt = (wp[1].transform.position - wp[0].transform.position).normalized;
        state = MovementStates.idle;       
        nextstate = Getdirect(vt);
        size = VTx_y(vt);
        TargetPos = TargetRay() + size;
        if (wp.Length % 2 == 0) f = WpEven;
        else f = WpOdd;
    }

    private void Update()
    {
        anim.SetInteger("State", (int)state);
    }
    void FixedUpdate()
    {
        if (Vector2.Distance(TargetPos,transform.position) <.01f)
        {
            StartCoroutine("Idle");
            ChangeVt(f);
            TargetPos = TargetRay() + size;           
        }
        if (!freeze)
        {
            speed += 0.5f;
            transform.position = Vector2.MoveTowards(transform.position, TargetPos, speed * Time.deltaTime);
        }
    }


    IEnumerator Idle()
    {        
        freeze = true;
        state = nextstate;
        speed = 0;
        yield return new WaitForSeconds(2);
        freeze = false;
    }

    private void SetBlink()
    {
        state = MovementStates.blink;
    }
    private void SetIdle()
    {
        state = MovementStates.idle;
    }
    
    private MovementStates Getdirect(Vector2 direction)
    {
        if (direction == Vector2.right) return MovementStates.right;
        else if (direction == Vector2.left) return MovementStates.left ;
        else if (direction == Vector2.up) return MovementStates.up;
        else if (direction == Vector2.down) return MovementStates.down ;
        else return MovementStates.idle ;
    }
    private Vector2 VTx_y(Vector2 vt)
    {
        if (vt == Vector2.right)
        {
            vt.y = 0; vt.x = x1;
        }
        else if (vt == Vector2.left)
        {
            vt.y = 0; vt.x = x2;
        }
        else if (vt == Vector2.up)
        {
            vt.y = y1; vt.x = 0;
        }
        else if (vt == Vector2.down)
        {
            vt.y = y2; vt.x = 0;
        }
        return vt;
    }
    private Vector2 TargetRay()
    {
        switch (p_h)
        {
            case true:
                rc = Physics2D.Raycast(transform.position, vt, Mathf.Infinity, Layers[0]).point;
                break;
            case false:
                rc = Physics2D.RaycastAll(transform.position, vt, Mathf.Infinity, Layers[0])[1].point;
                break;
        }
        return rc;
    }
    
    
    private void informchange()
    {
        switch (gameObject.name)
        {
            case "spike head":
                x1 = -1.4f; x2 = 1.3f;
                y1 = -1.3f; y2 = 1.4f;
                p_h = true;
                break;

            case "rock head":
                x1 = -1; x2 = 1;
                y1 = -1; y2 = 1;
                p_h = false;
                break;
        }
        
    }
    private void ChangeVt(PersonalFunction f)
    {
        temp = wp[currentIndex].transform.position;
        f();
        temp1 = wp[currentIndex].transform.position;
        if (temp.y == temp1.y)
        {
            size.y = 0;
            unit = player.size.x;
            switch (temp.x < temp1.x)
            {
                case true:
                    vt = Vector2.right;
                    nextstate = MovementStates.right;
                    size.x = x1; 
                    break;
                case false:
                    vt = Vector2.left;
                    nextstate = MovementStates.left;
                    size.x = x2;
                    break;
            }
        }
        else if (temp.x == temp1.x)
        {
            size.x = 0;
            unit = player.size.y;
            switch (temp.y < temp1.y)
            {
                case true:
                    vt = Vector2.up;
                    nextstate = MovementStates.up;
                    size.y = y1;
                    break;
                case false: 
                    vt = Vector2.down;
                    nextstate = MovementStates.down;
                    size.y = y2;
                    break;
            }
        }

    }

    private void WpEven()
    {
        currentIndex++;
        if (currentIndex >= wp.Length)
            currentIndex = 0;
    }
    private void WpOdd()
    {
        if (currentIndex == 0) cons = true;

        if (cons) currentIndex++;
        else currentIndex--;

        if (currentIndex >= wp.Length)
        {
            cons = false;
            currentIndex -=2;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            if (Physics2D.RaycastAll(transform.position, vt, 0.95f + unit , Layers[0])[1] && Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, vt, .1f, Layers[1]))
                collision.gameObject.GetComponent<Life>().Die();
        }
    }

}
