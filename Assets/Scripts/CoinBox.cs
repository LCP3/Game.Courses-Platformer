using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    [SerializeField] int _totalCoins = 3; //Adjustable coin count in box
    [SerializeField] Sprite _usedSprite;
    int _remainingCoins;

    private void Start()
    {
        _remainingCoins = _totalCoins;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>(); //Verify collision is a player
        if (player == null)
        {
            return;
        }

        if (collision.contacts[0].normal.y > 0 && _remainingCoins > 0) //Verify first contact is from below, and coins are still in the box
        {
            _remainingCoins--;
            if (_remainingCoins <= 0) //<= just a precautionary catch
            {
                GetComponent<SpriteRenderer>().sprite = _usedSprite;
            }
            Coin.CoinsCollected++;
        }

        
    }
}
