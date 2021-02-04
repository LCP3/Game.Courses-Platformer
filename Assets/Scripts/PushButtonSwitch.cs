using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] Sprite _downSprite;
    [SerializeField] UnityEvent _onEnter; //On enter, we want this event to happen

    private void OnTriggerEnter2D(Collider2D collision) // On collision
    {
        var player = collision.GetComponent<Player>(); //Get the player
        if (player == null) { //If no player
            return; //Exit
        }

        var spriteRenderer = GetComponent<SpriteRenderer>(); //Get spriteRenderer
        spriteRenderer.sprite = _downSprite; //Access sprite property

        _onEnter?.Invoke(); //Call any events for _onEnter

    }
}
