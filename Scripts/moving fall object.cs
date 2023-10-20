using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingfallobject : MonoBehaviour
{
    [SerializeField] private GameObject[] wp;
    [SerializeField] private float speed = 7f;
    private sbyte currentIndex = 0;

    void Update()
    {
        if (Vector2.Distance(wp[currentIndex].transform.position, transform.position) < .1f)
        {
            currentIndex++;
            if (currentIndex >= wp.Length)
                Invoke("FallWait", 1);
        }
        transform.position = Vector2.MoveTowards(transform.position, wp[currentIndex].transform.position, Time.deltaTime * speed);
    }
    void FallWait()
    {
        this.GetComponent<fall_plat>().Falling1();
    }
}
