using UnityEngine;

public class camera_controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform player;
    [SerializeField] private float minX, maxX, minY, maxY;
    private Vector3 tempos;

    // Update is called once per frame
    private void LateUpdate()
    {
        tempos = transform.position;

        tempos.x = player.position.x;
        tempos.y = player.position.y;

        if (tempos.x < minX) tempos.x = minX;
        if (tempos.x > maxX) tempos.x = maxX;
            
        if (tempos.y < minY) tempos.y = minY;
        if (tempos.y > maxY) tempos.y = maxY;

        transform.position = tempos;
    }

}
