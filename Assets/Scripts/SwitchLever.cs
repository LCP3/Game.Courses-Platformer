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

        // if (contactPoint.x > 0) // If the switch is pushed left, from the right side
        if (wasOnRight && playerWalkingRight)
         {
            SetLeverPosition(LeverDirection.Right);
         }
        
        // if (contactPoint.x < 0) // If pushed right, from left
        else if (wasOnRight == false && playerWalkingLeft)
         {
            SetLeverPosition(LeverDirection.Left);
        }
    }

    void SetLeverPosition(LeverDirection direction, bool setupLever = false) //Optional parameter to set up the starting position
    {
        if (setupLever == false && _currentDirection == direction)
            return;

        switch (direction)
        {
            case LeverDirection.Left:
                _spriteRenderer.sprite = _PushSwitchLeft;
                _onLeft.Invoke();
                break;
            case LeverDirection.Center:
                _spriteRenderer.sprite = _PushSwitchCenter;
                _onCenter.Invoke();
                break;
            case LeverDirection.Right:
                _spriteRenderer.sprite = _PushSwitchRight;
                _onRight.Invoke();
                break;
            default:
                break;
        }
    }

    private void OnValidate()
    {
        //_spriteRenderer = GetComponent<SpriteRenderer>(); // Cache our sprite renderer

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
        Debug.Log(_startingDirection);
    }

    public void LogUsingEvent()
    {
        Debug.Log("Using Event");
    }
}