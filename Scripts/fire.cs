using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    [SerializeField] private byte damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "player")
            collision.gameObject.GetComponent<Life>().Damaged(damage);
    }
}
