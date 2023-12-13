
using UnityEngine;

public class Floating_Item : MonoBehaviour
{
    private sbyte a;
    private Rigidbody2D rb;
    private Vector2 originalY,normal;

    private void Awake()
    {
        enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        originalY = transform.position;
        originalY.y += 0.2f;        
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, originalY) < .1f)
            a = -1;
        fObject(a);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        normal =collision.GetContact(0).normal;
        if (collision.gameObject.layer == 6 && normal == Vector2.up)
            enabled = true;   
    }
    private void fObject(sbyte a = 1)
    {
        transform.position = new Vector2(transform.position.x, originalY.y + Mathf.Sin(Time.time) * 0.2f * a);
    }
    // public bool Grounded()
    // {
    //     // return Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, .1f, 1<<6);
    //     return Physics2D.OverlapCircle(transform.position,5,1<<6);
    // }  
}
