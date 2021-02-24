using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSquish : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite _squishedSprite;

    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        BecomeSquished();
    }

    private void BecomeSquished()
    {
        _spriteRenderer.sprite = _squishedSprite;
    }
}
