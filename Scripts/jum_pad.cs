using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jum_pad : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D player_rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player" && collision.gameObject.GetComponent<playerMovement>().Grounded())
            anim.SetBool("collide",true);
    }

    private void UpWeGo()
    {
        player_rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
    }

    private void Idleback()
    {
        anim.SetBool("collide", false);
    }
}
