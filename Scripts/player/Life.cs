using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Life : MonoBehaviour
{
    [SerializeField] private GameObject heartb;
    private Animator anim;
    private Rigidbody2D rb;
    private bool died ;
    private Vector3 CheckPoint;
    [SerializeField] private AudioSource death_sound;
    public float health, maxhealth;
    public static event Action OnPlayerLife;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = maxhealth;
        CheckPoint = transform.position;
    }
    public void Damaged(byte amount)
    {
        health -= amount;
        OnPlayerLife?.Invoke();

        if (health <= 0)
        {
            health = 0;
            Die();
        }
        else StartCoroutine(DamagedAffect());
    }
    public void Healed(byte numb) 
    {
        if (health < maxhealth)
        {
            health += numb;
            OnPlayerLife?.Invoke();
        }
    }
    public void IncreaseMaxHP() 
    {
        maxhealth += 2;
        OnPlayerLife?.Invoke();
    }

    IEnumerator DamagedAffect()
    {
        anim.SetTrigger("damaged");
        Physics2D.IgnoreLayerCollision(7, 8);
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(7, 8,false);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("traps")) 
            Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("checkpoint"))
            CheckPoint = collision.transform.position;   
    }
    public void Die()
    {
        if(!died)
        {
        died = true;
        health = 0;
        OnPlayerLife?.Invoke();
        Physics2D.IgnoreLayerCollision(7, 0);
        Physics2D.IgnoreLayerCollision(7, 6);
        death_sound.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        }
    }
    private void restart()
    {
        died = false;
        health = maxhealth;
        OnPlayerLife?.Invoke();
        Physics2D.IgnoreLayerCollision(7, 0,false);
        Physics2D.IgnoreLayerCollision(7, 6,false);
        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.Play("player_idle");
        anim.ResetTrigger("death");
        transform.position = CheckPoint;
    }
}
