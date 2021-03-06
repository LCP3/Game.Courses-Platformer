using UnityEngine;
using UnityEngine.Events;

public class SwitchLever : MonoBehaviour
{
    [SerializeField] LeverDirection _startingDirection = LeverDirection.Center;

    [SerializeField] Sprite _PushSwitchLeft;
    [SerializeField] Sprite _PushSwitchRight;
    [SerializeField] Sprite _PushSwitchCenter;

    [SerializeField] UnityEvent _onLeft;
    [SerializeField] UnityEvent _onRight;
    [SerializeField] UnityEvent _onCenter;

    SpriteRenderer _spriteRenderer;
    AudioSource _audioSource;
    [SerializeField] AudioClip _audioSourceL;
    [SerializeField] AudioClip _audioSourceR;
    LeverDirection _currentDirection;

    enum LeverDirection
    { 
        Left,
        Center,
        Right,
    }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Cache our sprite renderer
        _audioSource = GetComponent<AudioSource>();
        SetLeverPosition(_startingDirection, true); // Set default Lever Position
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

        // if (contactPoint.x > 0) // If the switch is pushed right, from left
        if (wasOnRight == false && playerWalkingRight)
         {
            SetLeverPosition(LeverDirection.Right);
         }
        
        // if (contactPoint.x < 0) // If pushed left, from right
        else if (wasOnRight && playerWalkingLeft)
         {
            SetLeverPosition(LeverDirection.Left);
        }
    }

    void SetLeverPosition(LeverDirection direction, bool setupLever = false) //Optional parameter to set up the starting position
    {
        if (setupLever == false && _currentDirection == direction)
            return;

        _currentDirection = direction;
        switch (direction)
        {
            case LeverDirection.Left:
                _spriteRenderer.sprite = _PushSwitchLeft;
                _onLeft.Invoke();
                if (_audioSource != null)
                    _audioSource.PlayOneShot(_audioSourceL);
                break;
            case LeverDirection.Center:
                _spriteRenderer.sprite = _PushSwitchCenter;
                _onCenter.Invoke();
                break;
            case LeverDirection.Right:
                _spriteRenderer.sprite = _PushSwitchRight;
                _onRight.Invoke();
                if (_audioSource != null)
                    _audioSource.PlayOneShot(_audioSourceR);
                break;
            default:
                break;
        }
    }

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Cache our sprite renderer

        switch (_startingDirection)
        {
            case LeverDirection.Left:
                _spriteRenderer.sprite = _PushSwitchLeft;
                break;
            case LeverDirection.Center:
                _spriteRenderer.sprite = _PushSwitchCenter;
                break;
            case LeverDirection.Right:
                _spriteRenderer.sprite = _PushSwitchRight;
                break;
            default:
                break;
        }
    }
}