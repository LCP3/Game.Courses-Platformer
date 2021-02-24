using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour
{
    [SerializeField] Sprite _openMid;
    [SerializeField] Sprite _openTop;
    [SerializeField] SpriteRenderer _rendererMid;
    [SerializeField] SpriteRenderer _rendererTop;

    bool _doorOpened;

    [ContextMenu("Open Door")]
    public void SwitchOpen() //When our door opens
    {
        _doorOpened = true;
        _rendererMid.sprite = _openMid; //Change out the sprites
        _rendererTop.sprite = _openTop;


    }

    void Update()
    {

    }
}
