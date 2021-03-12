using UnityEngine;

public class KillOnEnter : MonoBehaviour 
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Initialize the player's collision
        var player = collision.collider.GetComponent<Player>();
        //If the player collides
        if (player != null)
        {
            player.ResetToStart();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Initialize the player's collision
        var player = collision.GetComponent<Player>();
        //If the player collides
        if (player != null)
        {
            player.ResetToStart();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        //Initialize the player's collision
        var player = other.GetComponent<Player>();
        //If the player collides
        if (player != null)
        {
            player.ResetToStart();
        }
    }
}
