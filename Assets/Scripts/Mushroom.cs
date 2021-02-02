using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null) {
            var rigidbody2d = player.GetComponent<Rigidbody2D>();
            if (rigidbody2d != null)
            { //In case the player doesn't have a rigidbody2D
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, _bounceVelocity); //Add Y velocity Vector2(x,y)
            }
        }
    }
}