using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] float _fallSpeed = 3;
    public bool PlayerTrigger;
    
    HashSet<Player> _playersInTrigger = new HashSet<Player>(); //HashSet requires SysCol, allows us to have an instance of each player
    Vector3 _initialPosition;
    bool _falling;

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

        while (wiggleTimer < 1f) //While we're wiggling
        {
            float randomY = UnityEngine.Random.Range(-0.05f, 0.05f);
            float randomX = UnityEngine.Random.Range(-0.05f, 0.05f);

            transform.position = _initialPosition + new Vector3(randomX, randomY); //Move our position a random distance from our initial position
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f);
            yield return new WaitForSeconds(randomDelay); //Wait for a random delay
            wiggleTimer += randomDelay; //Add to our wiggle timer
        }

        Debug.Log("Falling");
        _falling = true;

        foreach (var collider in GetComponents<Collider2D>()) //Get our colliders into an array, also can be written "Colliders[] collider ="
        {
            collider.enabled = false; //Turn off our colliders in the array
        }

        float fallTimer = 0;

        while (fallTimer < 3f)
        {
            transform.position += Vector3.down * Time.deltaTime* _fallSpeed; //Move down over time multiplied by our fall speed
            fallTimer += Time.deltaTime;
            Debug.Log(fallTimer);
            yield return null; //Wait until next frame           
        }
        Destroy(gameObject); //Destroy our platform after looping complete

    }

    private void OnTriggerExit2D(Collider2D collision) //On player exiting the platform trigger
    {
        if (_falling) //If we're falling, exit
        {
            return;
        }
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
