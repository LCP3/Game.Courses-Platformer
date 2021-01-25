using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 1;
    [SerializeField] float _jumpStrength = 200;

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * _speed; //Axis case-sensitive, goes from -1 to 1 from left to right, middle/resting is 0
        var rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = new Vector2(horizontal, rigidbody2d.velocity.y);

        var animator = GetComponent<Animator>(); //Get the Animator component
        bool walking = horizontal != 0; //If our horizontal speed isn't 0
        animator.SetBool("Walk", walking); // Walk


        if (horizontal != 0) {
            var spriteRenderer = GetComponent<SpriteRenderer>(); //Get the Sprite Renderer component
            spriteRenderer.flipX = horizontal < 0; //            
        }

        if (Input.GetButtonDown("Fire1")) {
            rigidbody2d.AddForce(Vector2.up * _jumpStrength);
        }

    }
}
