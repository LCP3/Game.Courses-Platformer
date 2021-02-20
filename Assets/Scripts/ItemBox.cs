using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject _item;
    [SerializeField] Vector2 _itemLaunchVelocity;
    
    bool _isEmpty = false;

    void Start()
    {
        if (_item != null)
        {
            _item.SetActive(false); //Turn off our item at start until we collide
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isEmpty)
            return;

        var player = collision.collider.GetComponent<Player>(); //Verify collision is a player
        if (player == null)
        {
            return;
        }

        if (collision.contacts[0].normal.y > 0) //Verify first contact is from below
        {
            GetComponent<SpriteRenderer>().sprite = _usedSprite;

            if (_item != null)
            {
                _isEmpty = true;
                _item.SetActive(true);
                var itemRigidBody = _item.GetComponent<Rigidbody2D>();
                {
                    itemRigidBody.velocity = _itemLaunchVelocity;
                }
            }
        }
    }
}
