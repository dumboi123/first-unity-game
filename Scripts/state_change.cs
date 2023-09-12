using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class state_change : MonoBehaviour
{
    private AudioSource end_sound;
    private bool check_collision = false;
    // Start is called before the first frame update
    private void Start()
    {
        end_sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player" && !check_collision)
        {   
            end_sound.Play();
            check_collision = true;
            Invoke("switchLevel", 5f);
        }
    }

    private void switchLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
