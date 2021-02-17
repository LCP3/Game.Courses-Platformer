using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    Collector _collector;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        gameObject.SetActive(false);
        _collector.ItemPickedUp();
    }

    internal void SetCollector(Collector collector)
    {
        _collector = collector; //Keep a copy of this variable for the life of the class, and store in a field
    }
}