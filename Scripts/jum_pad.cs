
using UnityEngine;

public class jum_pad : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D player_rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player" && collision.gameObject.GetComponent<PlayerStateManager>().Grounded())
            {
                anim.SetBool("collide",true);
                collision.gameObject.GetComponent<PlayerStateManager>()._animState = PlayerStateManager.MovementStates.jump;
            }
    }

    private void UpWeGo()
    {
        player_rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
    }

    private void Idleback()
    {
        anim.SetBool("collide", false);
    }
}
