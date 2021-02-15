using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] KeyLock _keyLock;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up; // (0,1,0)
        }

        var keyLock = collision.GetComponent<KeyLock>();
        if (keyLock != null && keyLock == _keyLock)
        {
            keyLock.Unlock(); //Run public Unlock() in KeyLock.cs
            Destroy(gameObject); //Destroy our key after use
        }
    }
}
