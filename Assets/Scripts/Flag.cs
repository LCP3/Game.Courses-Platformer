using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>(); //Get the Player component

        if (player == null) { //If player isn't the collider
            return; //Exit early
        }

        var animator = GetComponent<Animator>(); //Get the Animator
        animator.SetTrigger("Raise"); //Trigger the Raise animation
        //Play flag waving
        //Load new level
    }
}
