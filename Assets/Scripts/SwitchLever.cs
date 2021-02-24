using UnityEngine;
using UnityEngine.Events;

public class SwitchLever : MonoBehaviour
{
    [SerializeField] Sprite _PushSwitchLeft;
    [SerializeField] Sprite _PushSwitchRight;
    [SerializeField] UnityEvent _onPushLeft;
    [SerializeField] UnityEvent _onPushRight;

    SpriteRenderer _spriteRenderer;
    

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Cache our sprite renderer
    }

    void OnTriggerStay2D(Collider2D collision) // On trigger
    {
        var player = collision.GetComponent<Player>(); // Reference the player
        if (player == null) // If player is null, exit early
            return;
        
        var playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
            return;
        
        // Vector2 localVector = collision.gameObject.GetComponent<Collider2D>().ClosestPoint(transform.position); // Store a vector of the closest point on collision
        // var contactPoint = transform.InverseTransformPoint(localVector); // Inverse gives a clean, relative coordinate for the point of contact
        
        bool wasOnRight = collision.transform.position.x > transform.position.x;
        bool playerWalkingRight = playerRigidbody.velocity.x > 0;
        bool playerWalkingLeft = playerRigidbody.velocity.x < 0;

        // Debug.Log($"Contact at {contactPoint}");

        // if (contactPoint.x > 0) // If the switch is pushed left, from the right side
        if (wasOnRight && playerWalkingRight)
         {
             _spriteRenderer.sprite = _PushSwitchRight; // Change out the sprite
             _onPushLeft.Invoke(); //Invoke our UnityEvents, Door.Open(), Door.Close()
         }
        
        // if (contactPoint.x < 0) // If pushed right, from left
        else if (wasOnRight == false && playerWalkingLeft)
         {
             _spriteRenderer.sprite = _PushSwitchLeft; // Change out the sprite
             _onPushRight.Invoke(); // Invoke our UnityEvents, Door.Open(), Door.Close()
         }
    }
}