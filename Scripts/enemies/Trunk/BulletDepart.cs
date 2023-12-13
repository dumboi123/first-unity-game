using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletDepart : MonoBehaviour
{
    void Start() => StartCoroutine(Fading());

    IEnumerator Fading()
    {
        yield return new WaitForSeconds(2);
        GetComponentInParent<Animator>().Play("Unstable");
        yield return new WaitForSeconds(1.5f);
        Destroy(transform.parent.gameObject);
    }
}
