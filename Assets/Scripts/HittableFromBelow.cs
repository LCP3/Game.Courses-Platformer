using System;
using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite; //protected allows use in other files using inheritance -- private in essence
    Animator _animator;

    protected virtual bool CanUse => true; //Virtual property that tells us whether we can use the item (default false) || => shorthand, using a getter

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision) //On collision
    {
        if (CanUse == false) //Check if item is usable
            return;

        var player = collision.collider.GetComponent<Player>(); //Verify collision is a player
        if (player == null)
            return;

        if (collision.contacts[0].normal.y > 0) //Verify first contact is from below, and coins are still in the box
        {
            PlayAnimation();
            Use();
            if (CanUse == false) 
            {
                GetComponent<SpriteRenderer>().sprite = _usedSprite;
            }
        }
    }

    void PlayAnimation()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Use");
        }
    }

    protected virtual void Use() //protected as we want to modify it for our other classes, virtual lets us override.
    {
        Debug.Log($"Used {gameObject.name}");
    }
}
