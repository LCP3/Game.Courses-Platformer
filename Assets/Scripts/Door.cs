using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite _openMid;
    [SerializeField] Sprite _openTop;
    [SerializeField] SpriteRenderer _rendererMid;
    [SerializeField] SpriteRenderer _rendererTop;
    [SerializeField] Door _exit;


    int _requiredCoins = 2;

    [ContextMenu("Open Door")]
    void Open() //When our door opens
    {
        _rendererMid.sprite = _openMid; //Change out the sprites
        _rendererTop.sprite = _openTop;
    }

    void Update()
    {
        if (Coin.CoinsCollected >= _requiredCoins) { //If we collect enough coins
            Open(); //Open the door
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null && _exit != null)
        {
            player.TeleportTo(_exit.transform.position);
            Debug.Log("Teleport");
        }

    }
}
