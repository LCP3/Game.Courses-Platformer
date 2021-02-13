using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool PlayerTrigger;
    bool _falling;
    float _wiggleTimer = 0;

    HashSet<Player> _playersInTrigger = new HashSet<Player>(); //HashSet requires SysCol, allows us to have an instance of each player
    Vector3 _initialPosition;

    [Tooltip("Resets the wiggle timer when no players are on the platform.")]
    [SerializeField] bool _resetOnEmpty;
    [SerializeField] float _fallSpeed = 6;
    [Range(0.1f, 5)] [SerializeField] float _secondsBeforeFall = 3; //Range adds a slider in inspector
    [Range(0.005f, 0.1f)] [SerializeField] float _shakeX = 0.05f;
    [Range(0.005f, 0.1f)] [SerializeField] float _shakeY = 0.05f;
    private Coroutine _coroutine;

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
            _coroutine = StartCoroutine(WiggleAndFall()); //Set and start our coroutine
        }
    }

    IEnumerator WiggleAndFall()
    {
        Debug.Log("Waiting to wiggle");
        yield return new WaitForSeconds(0.25f); //Wait to wiggle
        Debug.Log("Wiggling");
        //_wiggleTimer = 0;

        while (_wiggleTimer < _secondsBeforeFall) //While we're wiggling
        {
            float randomY = UnityEngine.Random.Range(-_shakeX, _shakeY);
            float randomX = UnityEngine.Random.Range(-_shakeX, _shakeY);

            transform.position = _initialPosition + new Vector3(randomX, randomY); //Move our position a random distance from our initial position

            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f);
            yield return new WaitForSeconds(randomDelay); //Wait for a random delay

            _wiggleTimer += randomDelay; //Add to our wiggle timer
        }

        Debug.Log("Falling");
        _falling = true;
        Debug.Log(_playersInTrigger);


        foreach (var collider in GetComponents<Collider2D>()) //Get our colliders into an array, also can be written "Colliders[] collider ="
        {
            collider.enabled = false; //Turn off our colliders in the array
        }

        float fallTimer = 0;

        while (fallTimer < 3f)
        {
            transform.position += Vector3.down * Time.deltaTime* _fallSpeed; //Move down over time multiplied by our fall speed
            fallTimer += Time.deltaTime;
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
            
            StopCoroutine(_coroutine); // Stop our coroutine

            if (_resetOnEmpty)
            {
                _wiggleTimer = 0;
            }
        }
        
    }
}
