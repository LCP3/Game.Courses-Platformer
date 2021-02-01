using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 1;
    [SerializeField] float _jumpVelocity = 10;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;
    [SerializeField] float _downPull = 5;
    [SerializeField] float _maxJumpDuration = 0.1f;

    Vector3 _startPosition;
    int _jumpsRemaining;
    float _fallTimer;
    float _jumpTimer;
    

    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
    }

    void Update()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Default")); //Create overlapping circle to detect if we're grounded (point, radius(f for float), layer mask)
        bool isGrounded = hit != null; //if hit = null, false | if != null, true (Are we grounded?)


        var horizontal = Input.GetAxis("Horizontal") * _speed; //Axis case-sensitive, goes from -1 to 1 from left to right, middle/resting is 0
        var rigidbody2d = GetComponent<Rigidbody2D>();

        if (Mathf.Abs(horizontal) >= 1) //Mathf = Math w/ floating point
        { 
            rigidbody2d.velocity = new Vector2(horizontal, rigidbody2d.velocity.y);
        }

        var animator = GetComponent<Animator>(); //Get the Animator component
        bool walking = horizontal != 0; //If our horizontal speed isn't 0
        animator.SetBool("Walk", walking); // Walk


        if (horizontal != 0) {
            var spriteRenderer = GetComponent<SpriteRenderer>(); //Get the Sprite Renderer component
            spriteRenderer.flipX = horizontal < 0; //            
        }

        if (Input.GetButtonDown("Fire1") && _jumpsRemaining > 0)
        { // JUMP
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, _jumpVelocity); //Add Y velocity Vector2(x,y)
            _jumpsRemaining--; //Decrease jump count
            _fallTimer = 0; //Reset the fall timer
            _jumpTimer = 0; //Reset jump timer
        }
        else if (Input.GetButton("Fire1") && _jumpTimer <= _maxJumpDuration && _jumpsRemaining > 0) { //If Jump is held
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, _jumpVelocity); //Add Y velocity Vector2(x,y)
            _fallTimer = 0; //Reset the fall timer
            _jumpTimer += Time.deltaTime;
        }

        if (isGrounded) { // If we're grounded
            _fallTimer = 0; //Reset the fall timer
            _jumpsRemaining = _maxJumps; // Reset Jumps
        }
        else { //If we're NOT grounded
            //Fall down in increasing velocity
            _fallTimer += Time.deltaTime;
            var downForce = _downPull * _fallTimer * _fallTimer;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.velocity.y - downForce);
        }

    }

    internal void ResetToStart() //Internal means we can call this method from outside their own script (basically same as public)
    {
        transform.position = _startPosition;
    }
}
