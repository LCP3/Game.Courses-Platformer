using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 1;

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * _speed; //Axis case-sensitive, goes from -1 to 1 from left to right, middle/resting is 0
        var rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = new Vector2(horizontal, rigidbody2d.velocity.y);
    }
}
