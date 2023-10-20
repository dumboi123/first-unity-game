
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class fall_plat : MonoBehaviour
{
    private Animator anim;
    private RelativeJoint2D rj;
    private string tag;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rj = GetComponent<RelativeJoint2D>();
        tag = gameObject.tag;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player" && collision.gameObject.GetComponent<playerMovement>().Grounded())
        {
            if(tag != "mfplat")
            {
                anim.SetFloat("speed", 0.5f);
                anim.SetTrigger("stop");
                Invoke("Falling", 0.9f);
            }
            else
                StartCoroutine("SpringDuration");
        }
    }

    private void Falling()
    {
        GetComponent<RelativeJoint2D>().enabled = false;
        Destroy(gameObject, 4);
    }
    public void Falling1()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<movingfallobject>().enabled = false;
        Destroy(gameObject, 2);
    }


    IEnumerator SpringDuration()
    {
        yield return new WaitForSeconds(2);
        rj.enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.GetComponent<movingfallobject>().enabled = true;
    }

}
//anim.SetFloat("speed", 0.5f);
//anim.SetTrigger("stop");
//Invoke("Falling", 0.9f);