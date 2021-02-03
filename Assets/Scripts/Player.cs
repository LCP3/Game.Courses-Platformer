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
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    float _horizontal;
    bool _isGrounded;

    void Start()
    {
        _startPosition = transform.position; //Set starting position
        _jumpsRemaining = _maxJumps; //Initialize jump count
        _rigidbody2D = GetComponent<Rigidbody2D>(); //Cached GetComponent's -- only need it called once.
        _animator = GetComponent<Animator>(); //Get the Animator component
        _spriteRenderer = GetComponent<SpriteRenderer>(); //Get the SpriteRenderer component
    }

    void Update()
    {
        UpdateIsGrounded();

        ReadHorizontalInput();
        MoveHorizontal();
        UpdateAnimator();
        UpdateSpriteDirection();

        if (ShouldStartJump()) {
            Jump();
        }
        else if (ShouldContinueJump()) {
            ContinueJump();
        }

        _jumpTimer += Time.deltaTime; //Jump Timer constantly going, resets on jump.

        if (_isGrounded && _fallTimer > 0) { // If we're grounded
            _fallTimer = 0; //Reset the fall timer
            _jumpsRemaining = _maxJumps; // Reset Jumps
        }
        else { //If we're NOT grounded
            _fallTimer += Time.deltaTime; //Fall down in increasing velocity
            var downForce = _downPull * _fallTimer * _fallTimer; //Set downward force to Serialized Field downForce * _fallTimer^2 (increases gradually, exponentially)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y - downForce); //Keep X velocity the same, increase downward velocity
        }
    }

    void ContinueJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity); //Add Y velocity Vector2(x,y)
                                                                                     // _jumpsRemaining--; //Decrease jump count
        _fallTimer = 0; //Reset the fall timer
    }

    bool ShouldContinueJump()
    {
        return Input.GetButton("Fire1") && _jumpTimer <= _maxJumpDuration;
    }

    void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity); //Add Y velocity Vector2(x,y)
        _jumpsRemaining--; //Decrease jump count
        _fallTimer = 0; //Reset the fall timer
        _jumpTimer = 0; //Reset jump timer
    }

    bool ShouldStartJump()
    {
        return Input.GetButtonDown("Fire1") && _jumpsRemaining > 0;
    }

    void MoveHorizontal()
    {
        if (Mathf.Abs(_horizontal) >= 1) //Mathf = Math w/ floating point
        {
            _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
        }
    }

    void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis("Horizontal") * _speed; //Axis case-sensitive, goes from -1 to 1 from left to right, middle/resting is 0
    }

    void UpdateSpriteDirection()
    {
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = _horizontal < 0; //Turn sprite appropriately    
        }
    }

    void UpdateAnimator()
    {
        bool walking = _horizontal != 0; //If our horizontal speed isn't 0
        _animator.SetBool("Walk", walking); // Walk
    }

    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Default")); //Create overlapping circle to detect if we're grounded (point, radius(f for float), layer mask)
        _isGrounded = hit != null; //if hit = null, false | if != null, true (Are we grounded?)
    }

    internal void ResetToStart() //Internal means we can call this method from outside their own script (basically same as public)
    {
        transform.position = _startPosition;
    }
}
