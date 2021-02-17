using System;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    List<Collector> _collectors = new List<Collector>(); //List of type Collector, new List of Collectors instantiated since we're not Serializing

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        gameObject.SetActive(false);

        foreach (var collector in _collectors)
        {
            collector.ItemPickedUp();
        }
        
    }

    internal void AddCollector(Collector collector)
    {
        _collectors.Add(collector); //Add collector to our list of collectors
    }
}