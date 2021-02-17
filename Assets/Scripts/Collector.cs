﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{

    [SerializeField] List<Collectible> _collectibles; //Hashset of collectibles named _collectibles
    [SerializeField] UnityEvent _onCollectionComplete; //UnityEvent holds trigger for the door

    TMP_Text _remainingCounter;

    int _countCollected;

    // Start is called before the first frame update
    void Start()
    {
        _remainingCounter = GetComponentInChildren<TMP_Text>();

        foreach (var collectible in _collectibles) //For each collectible in the List
        {
            collectible.AddCollector(this); //Set collectible to (this)
        }

        int countRemaining = _collectibles.Count - _countCollected;
        _remainingCounter?.SetText(countRemaining.ToString());
    }

    public void ItemPickedUp()
    {
        _countCollected++; //Increment our count collected
        int countRemaining = _collectibles.Count - _countCollected;
        _remainingCounter?.SetText(countRemaining.ToString()); //Set the counter's text to our remaining amount of collectibles -- the ? is shorthand for != null (evaluates true)

        if (countRemaining > 0)
        {
            return;
        }

        _onCollectionComplete.Invoke(); //Invoke our unity event, opening the door and removing the canvas.
    }

    //Called in the editor only when we change a value in the inspector
    void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList(); //Turn the .Distinct (Linq statement that finds only the distinct/unique entries) into a List, preventing duplicates
    }
}
