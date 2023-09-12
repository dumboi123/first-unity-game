using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_block : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            anim.SetInteger("State", 1);
        }
    }
    private void FireOn()
    {
        anim.SetInteger("State", 2);
    }
    private void PreOff()
    {
        anim.SetInteger("State", 3);
    }
    private void FireOff()
    {
        anim.SetInteger("State", 0);
    }
}
