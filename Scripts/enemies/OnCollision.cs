using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private string _objectScriptName;
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        Vector2 collisionNormal = collision.contacts[0].normal;                                      
        if(collision.gameObject.tag == "Immortal" || (DotTest(collisionNormal) && collision.gameObject.CompareTag("Player")))
        { 
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            _anim.SetTrigger("Hit");
            return ;
        }
    }

    private bool DotTest(Vector2 collisionNor) => Vector2.Dot(collisionNor, Vector2.down) > 0.25f;
    public void SetDead() => GetComponent<DeathAnimation>().enabled = true;
    public void DisableMovement() => (GetComponent(_objectScriptName) as MonoBehaviour).enabled = false;              // MARVELOUS
}
