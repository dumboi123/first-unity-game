using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Floating_Item : MonoBehaviour
{
    private sbyte a;
    private Rigidbody2D rb;
    private Vector2 originalY;
    public float floatStrength;
    private void Awake()
    {
        enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        originalY = transform.position;
        Debug.Log(originalY);
        originalY.y += 0.2f;        
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, originalY) < .1f)
            a = -1;
        fObject(a);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
            enabled = true;   
    }
    private void fObject(sbyte a = 1)
    {
        transform.position = new Vector2(transform.position.x, originalY.y + Mathf.Sin(Time.time) * 0.2f * a);
    }
}
