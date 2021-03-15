using System;
using UnityEngine;

public abstract class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite; //protected allows use in other files using inheritance -- private in essence
    Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] AudioClip _boxNotEmpty;

    protected virtual bool CanUse => true; //Virtual property that tells us whether we can use the item (default false) || => shorthand, using a getter

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision) //On collision
    {
        Debug.Log("Collision");
        if (CanUse == false) //Check if item is usable
            return;

        var player = collision.collider.GetComponent<Player>(); //Verify collision is a player
        if (player == null)
            return;

        if (collision.contacts[0].normal.y > 0) //Verify first contact is from below, and coins are still in the box
        {
            PlayAnimation();
            Use();
            _audioSource.PlayOneShot(_boxNotEmpty);
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

    protected abstract void Use(); //protected as we want to modify/access it for our other classes, virtual lets us override/update, abstract at a method level requires the use of this method by subclasses.
    /*{
        //Debug.Log($"Used {gameObject.name}");
    }*/
}
