using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject BulletPart;
    private sbyte x = 1;
    void Start()
    {
        if(transform.localScale.x < 0) x=-1;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("wall") || collider.CompareTag("Player"))
        {
            GameObject Fragment = Instantiate(BulletPart,transform.position, Quaternion.identity);
            Fragment.transform.localScale *= x;
            Fragment.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(5,0),ForceMode2D.Impulse);
            Destroy(gameObject);
        }
        else 
            Destroy(gameObject);
            
    }


}
