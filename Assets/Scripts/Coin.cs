using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    static int _coinsCollected; //Static = only one instance of this variable across all game objects

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player == null) {
            return;
        }

        gameObject.SetActive(false); //Turn off coin
        _coinsCollected++; //Add to coin counter
        Debug.Log(_coinsCollected);
    }
}
