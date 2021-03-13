using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerNum = 1;
    [Header("Movement")]
    [SerializeField] float _speed = 1;
    [SerializeField] float _slipFactor = 1;
    [Header("Jump")]
    [SerializeField] float _jumpVelocity = 10;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;
    [SerializeField] Transform _leftSensor;
    [SerializeField] Transform _rightSensor;
    [SerializeField] float _downPull = 5;
    [SerializeField] float _maxJumpDuration = 0.1f;
    [SerializeField] float _wallslideSpeed = 1f;


    Vector3 _startPosition;
    int _jumpsRemaining;
    float _fallTimer;
    float _jumpTimer;
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    float _horizontal;
    bool _isGrounded;
    bool _isSlipperySurface;
    string _jumpButton;
    string _horizontalAxis;
    int _layerMask;
    AudioSource _audioSource;
    public AudioClip[] _audioClip;
    private bool _playerLanded;

    public int PlayerNumber => _playerNum; //Shorthand for getter

    void Start()
    {
        _startPosition = transform.position; //Set starting position
        _jumpsRemaining = _maxJumps; //Initialize jump count
        _rigidbody2D = GetComponent<Rigidbody2D>(); //Cached GetComponent's -- only need it called once.
        _animator = GetComponent<Animator>(); //Get the Animator component
        _spriteRenderer = GetComponent<SpriteRenderer>(); //Get the SpriteRenderer component
        _jumpButton = $"P{_playerNum}Jump"; //Cache string interpolation for performance
        _horizontalAxis = $"P{_playerNum}Horizontal"; //Performance cache
        _layerMask = LayerMask.GetMask("Default"); //Performance cache
        _audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();


        if (_isSlipperySurface)
        {
            SlipHorizontal();
        }
        else
        {
            MoveHorizontal();
        }

        UpdateAnimator();
        UpdateSpriteDirection();

        if (ShouldSlide())
        {
            if (ShouldStartJump())
                WallJump();
            else
                Slide();
            _jumpsRemaining = 1; //Jump after wallslide
            return;
        }
        if (ShouldStartJump())
        {
            Jump();
        }
        else if (ShouldContinueJump())
        {
            ContinueJump();
        }

        _jumpTimer += Time.deltaTime; //Jump Timer constantly going, resets on jump.

        if (_isGrounded && _fallTimer > 0)
        { // If we're grounded
            _fallTimer = 0; //Reset the fall timer
            _jumpsRemaining = _maxJumps; // Reset Jumps

            if (_audioSource.isPlaying || _playerLanded) //If there's a landing sound already playing, or if the player has already landed, exit early
                return;
            PlayLandingClip();
        }
        else
        { //If we're NOT grounded
            _fallTimer += Time.deltaTime; //Fall down in increasing velocity
            var downForce = _downPull * _fallTimer * _fallTimer; //Set downward force to Serialized Field downForce * _fallTimer^2 (increases gradually, exponentially)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y - downForce); //Keep X velocity the same, increase downward velocity

            if (_playerLanded && _fallTimer > .1) //If the player is transitioning from landing to jumping, and has fallen for a short period
                _playerLanded = false; //Set up to play the landing sfx
        }
    }

    private void WallJump()
    {
        _rigidbody2D.velocity = new Vector2(-_horizontal * _jumpVelocity, _jumpVelocity * 1.5f); //Jump up and away from the direction of the wall
    }

    void Slide()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_wallslideSpeed);
    }

    bool ShouldSlide()
    {
        if (_isGrounded)
            return false;

        if (_rigidbody2D.velocity.y > 0)
            return false;

        if (_horizontal < 0) // If pushing to the left
        {
            var hit = Physics2D.OverlapCircle(_leftSensor.position, 0.1f);

            if (hit != null && hit.CompareTag("Wall"))
                return true;
        }

        if (_horizontal > 0) // If pushing to the right
        {
            var hit = Physics2D.OverlapCircle(_rightSensor.position, 0.1f);

            if (hit != null && hit.CompareTag("Wall"))
                return true;
        }

        return false;
    }

    private void PlayLandingClip()
    {
        if (_rigidbody2D.velocity.y > 0) //If the player is still moving up, exit early
            return;
        _audioSource.PlayOneShot(_audioClip[0]); //Play landing sfx
        _playerLanded = true; //Reset grounded landed state
    }

    void ContinueJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity); //Add Y velocity Vector2(x,y)
                                                                                     // _jumpsRemaining--; //Decrease jump count
        _fallTimer = 0; //Reset the fall timer
    }

    bool ShouldContinueJump()
    {
        return Input.GetButton(_jumpButton) && _jumpTimer <= _maxJumpDuration;
    }

    void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity); //Add Y velocity Vector2(x,y)
        _jumpsRemaining--; //Decrease jump count
        _fallTimer = 0; //Reset the fall timer
        _jumpTimer = 0; //Reset jump timer
        
        _audioSource?.Play();
    }

    bool ShouldStartJump()
    {
        return Input.GetButtonDown(_jumpButton) && _jumpsRemaining > 0;
    }

    void MoveHorizontal()
    {
        float newHorizontal = Mathf.Lerp(//Linear interpolation 
            _rigidbody2D.velocity.x, //Between two values
            _horizontal * _speed,
            Time.deltaTime); //Over a duration, going to our desired velocity
        _rigidbody2D.velocity = new Vector2(newHorizontal, _rigidbody2D.velocity.y);
    }

    void SlipHorizontal()
    {
        var desiredVelocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
        var smoothedVelocity = Vector2.Lerp( //Linear interpolation smoothed over one second
            _rigidbody2D.velocity,
            desiredVelocity,
            Time.deltaTime / _slipFactor);

        _rigidbody2D.velocity = smoothedVelocity;
    }

    void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis(_horizontalAxis) * _speed; //Axis case-sensitive, goes from -1 to 1 from left to right, middle/resting is 0
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
        _animator.SetBool("Jump", ShouldContinueJump());
        _animator.SetBool("Wallslide", ShouldSlide());

    }

    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, _layerMask); //Create overlapping circle to detect if we're grounded (point, radius(f for float), layer mask)
        _isGrounded = hit != null; //if hit = null, false | if != null, true (Are we grounded?)

        if (hit != null) //Check we're grounded, if so
        {
            _isSlipperySurface = hit.CompareTag("Slippery"); //Set Slippery to true

        }
        else
        {
            _isSlipperySurface = false; //Set Slippery off
        }
    }

    internal void ResetToStart() //Internal means we can call this method from outside their own script (basically same as public)
    {
        _rigidbody2D.position = _startPosition;
        SceneManager.LoadScene("Menu");
    }

    internal void TeleportTo(Vector3 position)
    {
        _rigidbody2D.position = position;
        _rigidbody2D.velocity = Vector2.zero;
    }

}
