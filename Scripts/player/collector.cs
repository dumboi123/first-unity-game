using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class collector : MonoBehaviour
{
    private Color Original;
    private PlayerStateManager player;
    private short cherries = 0;
    private Life life;
    [SerializeField] private Text cherriestext;
    [SerializeField] private AudioSource collect_sound;
    private void Start()
    {
        life = GetComponent<Life>();
        Original = GetComponent<Renderer>().material.color;
        player = GetComponent<PlayerStateManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            // case "collectable":
            //     Collected(collision);
            //     cherries++;
            //     cherriestext.text = "        X " + cherries;
            //     break;
            case "HP":
                Collected(collision);
                life.Healed(2);
                break;
            case "HHP":
                Collected(collision);
                life.Healed(1);
                break;
            case "MHP":
                Collected(collision);
                life.IncreaseMaxHP();
                break;
            case "PWU":
                Collected(collision);
                StartCoroutine("SpeedUp");
                break;

        }
    }
        /*if (collision.gameObject.CompareTag("collectable"))
        {
            Collected(collision);
            cherries++;
            cherriestext.text = "        X " + cherries;
        }
        else if (collision.gameObject.CompareTag("HP"))
        {
            Collected(collision);
            life.Healed(2);
        }
        else if (collision.gameObject.CompareTag("HHP"))
        {
            Collected(collision);
            life.Healed(1);
        }
        else if (collision.gameObject.CompareTag("MHP"))
        {
            Collected(collision);
            life.IncreaseMaxHP();
        }*/

    private void Collected(Collider2D collision)
    {
        Destroy(collision.gameObject);
        //collect_sound.Play();
    }

    IEnumerator SpeedUp()
    {
        gameObject.tag = "immortal";
        player.SetSpeed(12);
        player.SetJumpforce(20);
        GetComponent<TrailRenderer>().enabled = true;
        GetComponent<Renderer>().material.color = new Color(5,11,4);
        yield return new WaitForSeconds(5);
        gameObject.tag = "Untagged";
        player.SetSpeed(6);
        player.SetJumpforce(14);
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<Renderer>().material.color = Original;

    }
}
