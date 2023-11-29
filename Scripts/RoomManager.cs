
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject VirtuarlCamera;
    private void OnTriggerEnter2D (Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger)
            VirtuarlCamera.SetActive(true);
    }
    private void OnTriggerExit2D (Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger)
            VirtuarlCamera.SetActive(false);
    }
}
