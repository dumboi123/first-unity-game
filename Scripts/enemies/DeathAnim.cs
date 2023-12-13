
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    float _elapsed = 0;
    byte _duration =3;
    float _spin = 0;

    private void OnEnable(){
        UpdateSprite();
        StartCoroutine("Animate");
    }

    private void UpdateSprite() => GetComponent<SpriteRenderer>().sortingOrder = 10;

    private IEnumerator Animate(){

        Vector3 velocity = Vector3.up * 10;
        while(_elapsed<_duration)
        {
            _spin -= Time.deltaTime * 100;
            transform.position += velocity* Time.deltaTime;
            transform.rotation = Quaternion.Euler(0,0,_spin);
            velocity.y += -36 * Time.deltaTime;
            _elapsed += Time.deltaTime;
            yield return null;
        }
        if(transform.parent.gameObject != null) Destroy(transform.parent.gameObject);
        else Destroy(gameObject);
    }
}
    // private void DisablePhysics(){
    //     Collider2D collider = GetComponent<Collider2D>();
    //     collider.enabled = false;
    //     GetComponent<Rigidbody2D>().isKinematic =  true;
    // }