
using System.Collections;
using UnityEngine;

public class fire_block : MonoBehaviour
{
    private Animator anim;
    private bool fired;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "player" && !fired)
        {
            anim.SetInteger("State", 1);
            fired = true;
        }
    }
    private void FireOn()    
    {
        // anim.SetInteger("State", 2);
        StartCoroutine("burning");
    }
    private void PreOff()
    {
        // Debug.Log("PreOff");
        anim.SetInteger("State", 3);
    }
    private void FireOff()
    {
        anim.SetInteger("State", 0);
        fired = false;
    }
    IEnumerator burning(){
        anim.SetInteger("State",2);
        yield return new WaitForSeconds(2);
        anim.SetInteger("State",3);
    }
    
}
