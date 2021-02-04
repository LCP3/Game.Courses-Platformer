using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform _leftSensor; // Give our sensors a reference to access the properties
    [SerializeField] Transform _rightSensor;
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

        player.ResetToStart();
    }
}
