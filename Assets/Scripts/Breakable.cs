using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Breakable : MonoBehaviour, ITakeDamage
{
    public void TakeDamage()
    {
        TakeHit();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() == null) 
        {
            return;
        }

        if (collision.contacts[0].normal.y > 0) //If hit by player from below on first contact
        {
            TakeHit();
        }
    }

    void TakeHit()
    {
        var particleSystem = GetComponent<ParticleSystem>(); //Get our particle system
        particleSystem.Play(); //Play bricks breaking
        GetComponent<AudioSource>().Play(); //Play sfx

        GetComponent<Collider2D>().enabled = false; //Turn off collider
        GetComponent<SpriteRenderer>().enabled = false; //Turn off sprite renderer
    }
}
