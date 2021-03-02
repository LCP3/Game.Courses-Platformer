using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected; //Static = only one instance of this variable across all game objects

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player == null) {
            return;
        }

        gameObject.SetActive(false); //Turn off coin
        CoinsCollected++; //Add to coin counter
        ScoreSystem.Add(100); //Add 100 points
    }
}
