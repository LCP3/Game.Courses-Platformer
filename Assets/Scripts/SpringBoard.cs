using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 10;
    [SerializeField] Sprite _downSprite;

    SpriteRenderer _spriteRenderer;
    Sprite _upSprite;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();  //Get SpriteRenderer in Awake (cached)
        _upSprite = _spriteRenderer.sprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            var rigidbody2d = player.GetComponent<Rigidbody2D>();
            if (rigidbody2d != null)
            { //In case the player doesn't have a rigidbody2D
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, _bounceVelocity); //Add Y velocity Vector2(x,y)
                _spriteRenderer.sprite = _downSprite;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null) {
            _spriteRenderer.sprite = _upSprite;
        }
    }
}
