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
    [SerializeField] Canvas _canvas;
    [SerializeField] int _requiredCoins = 2;

    bool _doorOpened;

    [ContextMenu("Open Door")]
    public void Open() //When our door opens
    {
        _doorOpened = true;
        _rendererMid.sprite = _openMid; //Change out the sprites
        _rendererTop.sprite = _openTop;

        if (_canvas != null) { //If the object has a canvas
            _canvas.enabled = false; //Turn off the door counter UI
        }
    }

    void Update()
    {
        if (_doorOpened == false && Coin.CoinsCollected >= _requiredCoins) { //If we collect enough coins
            Open(); //Open the door
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_doorOpened == false) { //If door isn't open
            return; //Exit early
        }

        var player = collision.GetComponent<Player>();
        if (player != null && _exit != null) //If player and exit are assigned
        {
            player.TeleportTo(_exit.transform.position); //Teleport to the position of the _exit door.
        }

    }
}
