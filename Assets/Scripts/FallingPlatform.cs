using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool PlayerTrigger;
    
    HashSet<Player> _playersInTrigger = new HashSet<Player>(); //HashSet requires SysCol, allows us to have an instance of each player
    Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision) //On player entering the platform trigger
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        _playersInTrigger.Add(player); //Add the player to the hash set

        PlayerTrigger = true;

        if (_playersInTrigger.Count == 1) 
        {
            StartCoroutine(WiggleAndFall());
        }
    }

    IEnumerator WiggleAndFall()
    {
        Debug.Log("Waiting to wiggle");
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Wiggling");
        float wiggleTimer = 0;

        while (wiggleTimer < 1)
        {
            float randomY = UnityEngine.Random.Range(-0.01f, 0.01f);
            float randomX = UnityEngine.Random.Range(-0.01f, 0.01f);

            transform.position = _initialPosition + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f);
            yield return new WaitForSeconds(randomDelay);
            wiggleTimer += randomDelay;
        }

        Debug.Log("Falling");
        yield return new WaitForSeconds(3f);

    }

    private void OnTriggerExit2D(Collider2D collision) //On player exiting the platform trigger
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        _playersInTrigger.Remove(player); //Remove the player from the hash set

        if (_playersInTrigger.Count == 0) //If there are 0 players in the trigger
        {
            PlayerTrigger = false; //Toggle
            StopCoroutine(WiggleAndFall()); // Stop our coroutine
        }
        
    }
}
