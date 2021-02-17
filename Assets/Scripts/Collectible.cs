using System;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //List<Collector> _collectors = new List<Collector>(); //List of type Collector, new List of Collectors instantiated since we're not Serializing

    public event Action OnPickedUp; //Event with the Action delegation

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        gameObject.SetActive(false);

        OnPickedUp?.Invoke(); //Need a question mark/if due to event defaulting to null

        /*if (OnPickedUp != null)
        {
            OnPickedUp.Invoke();
        }*/
        
    }
}