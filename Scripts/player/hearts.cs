using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hearts : MonoBehaviour
{
    [SerializeField] private Sprite empty, half, full;
    Image IMG;
    // Start is called before the first frame update
    // awake and start
    private void Awake()
    {
        IMG = GetComponent<Image>();
    }
    public void  displayHeart( Heartstatus status)
    {
        switch (status)
        {
            case Heartstatus.Empty:
                IMG.sprite = empty;
                break;
            case Heartstatus.Half:
                IMG.sprite = half;
                break;
            case Heartstatus.Full:
                IMG.sprite = full;
                break;
        }
    }
    // Update is called once per frame
    public enum Heartstatus
    {
        Empty, Half, Full
    }
}
