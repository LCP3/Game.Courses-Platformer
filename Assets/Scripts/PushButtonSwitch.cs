using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] Sprite _pressedSprite;
    [SerializeField] UnityEvent _onPressed; //On press, we want this event to happen
    [SerializeField] UnityEvent _onReleased; //On release, we want this event to happen
    SpriteRenderer _spriteRenderer;
    Sprite _releasedSprite;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); //Get spriteRenderer -- Cached
        _releasedSprite = _spriteRenderer.sprite;

        BecomeReleased();
    }
    void OnTriggerEnter2D(Collider2D collision) // On collision
    {
        var player = collision.GetComponent<Player>(); //Get the player
        if (player == null)
        { //If no player
            return; //Exit
        }

        BecomePressed();
    }
    void BecomePressed()
    {
        _spriteRenderer.sprite = _pressedSprite; //Access sprite property
        _onPressed?.Invoke(); //Call any events for _onEnter
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>(); //Get the player
        if (player == null)
        { //If no player
            return; //Exit
        }

        BecomeReleased();
    }
    void BecomeReleased()
    {
        _spriteRenderer.sprite = _releasedSprite; //Access sprite property
        _onReleased?.Invoke(); //Call any events for _onEnter
    }
}
