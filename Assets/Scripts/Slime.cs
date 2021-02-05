using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform _leftSensor; // Give our sensors a reference to access the properties
    [SerializeField] Transform _rightSensor;
    [SerializeField] Sprite _deadSprite;

    Rigidbody2D _rigidbody2D;
    float _direction = -1;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = new Vector2(_direction, _rigidbody2D.velocity.y);

        if (_direction < 0)
        {
            ScanSensor(_leftSensor);
        }
        else {
            ScanSensor(_rightSensor);
        }
    }

    void ScanSensor(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);  //Visual representation of RayCasted line downward for debugging purposes
        
        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f); //Physics2D.Raycast(origin, direction, distance)
        if (result.collider == null)
        {
            TurnAround();
        }

        Debug.DrawRay(sensor.position, new Vector2(_direction, 0) * 0.1f, Color.red);  //Visual representation of RayCasted line in the movement direction for debugging purposes

        var sideResult = Physics2D.Raycast(sensor.position, new Vector2(_direction, 0), 0.1f); //Physics2D.Raycast(origin, direction, distance)
        if (sideResult.collider != null)
        {
            TurnAround();
        }
    }

    void TurnAround()
    {
        _direction *= -1; //Flip direction
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = _direction > 0; //Flip sprite
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null) {
            return;
        }

        var contact = collision.contacts[0];
        Vector2 normal = contact.normal;
        Debug.Log($"Normal = {normal}");

        if (normal.y <= -0.5)
        {
            StartCoroutine(Die());
        }
        else
        {
            player.ResetToStart();
        }
    }

    IEnumerator Die()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        
        spriteRenderer.sprite = _deadSprite; //Get our sprite renderer and immediately set a property's value.  Only making the call one time.
        GetComponent<Animator>().enabled = false; //Turn off our animation
        GetComponent<Collider2D>().enabled = false; //Turn off our capsule collider (Collider2D is the base class of CapsuleCollider2D)
        this.enabled = false; //Turn off Slime.cs
        GetComponent<Rigidbody2D>().simulated = false; //Turn off our physics.

        float alpha = 1;

        while (alpha > 0) //While alpha is greater than 0
        {
            yield return null; //Wait until next frame
            alpha -= Time.deltaTime; //Decrement alpha by Time.deltaTime
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }        
    }
}
