using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall_plat : MonoBehaviour
{
    private Animator anim;
    private bool playerOn = false;
    [SerializeField] private GameObject[] wp;
    [SerializeField] private float speed = 7f;
    private sbyte currentIndex = 1;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player" && collision.gameObject.GetComponent<playerMovement>().Grounded() ) 
        {
            anim.SetFloat("speed", 0.5f);
            anim.SetTrigger("stop");
            Invoke("Falling", 0.9f);
        }                
    }

    private void Falling()
    {
        GetComponent<RelativeJoint2D>().enabled = false;
        Destroy(gameObject, 2);
    }
}
