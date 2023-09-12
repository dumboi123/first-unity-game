//using System.Collections;
//using System.Collections.Generic;
//using System.Security.Cryptography;
//using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class spiked_head : MonoBehaviour
{
    [SerializeField] private GameObject[] wp;
    [SerializeField] private LayerMask Wall;
    private Rigidbody2D rb;
    private Vector2 vt,temp,temp1,contactpoint;
    private sbyte currentIndex = 1;
    private int contactTotal;
    private bool freeze, dk;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vt = Vector2.right;
    }
    private void FixedUpdate()
    {
        if (!freeze)
        {
            rb.AddForce(vt);
        }

        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("wall"))
        {
            contactTotal = collision.contactCount;
            for (int i = 0; i < contactTotal; i++)
            {   
                if(contactpoint == Vector2.zero || contactpoint != collision.contacts[i].normal)
                    contactpoint = collision.contacts[i].normal;
            }
            if (DotTest(contactpoint, vt))
            {
                Debug.Log(transform.position.y);
                rb.velocity = Vector2.zero;
                Debug.Log("VECTOR before: " + vt);
                ChangeVt();
                //StartCoroutine("Freezed");
                Debug.Log("CONTACT POINT: " + contactpoint);
                Debug.Log("INDEX before: " + temp);
                Debug.Log("INDEX after: " + temp1);
                Debug.Log("VECTOR after: " + vt);

            }
        }
    }

    IEnumerator Freezed()
    {
        freeze = true;
        yield return new WaitForSeconds(2);
        freeze = false;
    }


    private bool DotTest(Vector2 collisionNor, Vector2 a)
    {
        return Vector2.Dot(collisionNor, a) < 0;
    }

    private void ChangeVt()
    {
        temp = wp[currentIndex].transform.position;
        currentIndex++;
        if (currentIndex >= wp.Length)
            currentIndex = 0;
        temp1 = wp[currentIndex].transform.position;
        if (temp.y == temp1.y)
            {
                switch (temp.x < temp1.x)
                {
                    case true:
                        vt = Vector2.right;
                        break;
                    case false:
                        vt = Vector2.left;
                    break;
                }
            }
        else if (temp.x == temp1.x)
            {
                switch (temp.y < temp1.y)
                {
                    case true:
                        vt = Vector2.up;
                        break;
                  case false:
                        vt = Vector2.down;
                        break;
                }
            }

    }

}
