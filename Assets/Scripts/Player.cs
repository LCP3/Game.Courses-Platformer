﻿using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 1;
    [SerializeField] float _jumpStrength = 200;
    Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
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

        if (Input.GetButtonDown("Fire1")) {
            rigidbody2d.AddForce(Vector2.up * _jumpStrength);
        }

    }

    internal void ResetToStart() //Internal means we can call this method from outside their own script (basically same as public)
    {
        transform.position = _startPosition;
    }
}
