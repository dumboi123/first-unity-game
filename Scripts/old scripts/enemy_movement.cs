using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    [SerializeField] private GameObject[] wp;
    [SerializeField] private float speed = 3f, chasezone = 0;
    [SerializeField] private Transform player;
    private Animator anim;
    private SpriteRenderer spr;
    private sbyte currentIndex = 0;
    private bool freeze,ischasing,chased,stun,running;
    private void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();   
    }
    void Update()
    {
        ischasing= (Vector2.Distance(transform.position, player.position) <= chasezone) ? true : false;
        if (ischasing)
        {
            chased = true;
            if (transform.position.x < player.position.x)
                transform.localScale = new Vector2(-1, 1);
            else if (transform.position.x > player.position.x)
                transform.localScale = Vector2.one;
            anim.SetInteger("state", 1);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);                
        }
        else
        {
            if (chased)
            {
                stunning();
                Invoke("afterstunned", 5);
            }
            else
            {
                Debug.Log("PATROL 2");
                Patrol();
            }
        }
    }
    IEnumerator idle()
    {
        freeze = true;
        anim.SetInteger("state", 0);
        yield return new WaitForSeconds(2);
        if (currentIndex == 1)
            transform.localScale = new Vector2(-1, 1);
        else if (currentIndex == 0)
            transform.localScale = Vector2.one;
        freeze = false;
    }
    private void stunning()
    {
        if (!stun)
        {
            Debug.Log("CurrentIndex "+currentIndex);
            freeze = true;
            anim.SetInteger("state", 0);
            InvokeRepeating("Fliping", 3, 1);
            stun = true;
        }
    }
    private void afterstunned()
    {
        CancelInvoke("Fliping");
        freeze = false;
        if (OutRange(wp[0].transform.position, wp[1].transform.position))
        {
            Debug.Log("CurrentIndex " + currentIndex);
            ReturnToPatrol();
        }
    }
    private void Fliping()
    {
        spr.flipX = !spr.flipX;
    }
    private void Patrol()
    {
        if (Vector2.Distance(wp[currentIndex].transform.position, transform.position) < .1f)
        {
            StartCoroutine("idle");
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
    private bool OutRange( Vector2 wp1, Vector2 wp2)
    {
        return (transform.position.x < wp1.x || transform.position.x >wp2.x);
    }
    private void ReturnToPatrol()
    {
        if (!freeze) 
        {
            if (!running)
            {
                anim.SetInteger("state", 1);
                running = true;
            }
            Vector3 direction = (wp[currentIndex].transform.position - transform.position).normalized;
            if (direction.x > 0)
                transform.localScale = new Vector2(-1, 1);
            else if (direction.x < 0)                
                transform.localScale = Vector2.one;
            chased = (Vector2.Distance(transform.position, wp[0].transform.position) < .1f || Vector2.Distance(transform.position, wp[1].transform.position) < .1f) ? false : true;
            transform.position = Vector2.MoveTowards(transform.position, wp[currentIndex].transform.position, Time.deltaTime * speed);
        }
        
    }
}
