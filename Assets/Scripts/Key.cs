using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] KeyLock _keyLock;

    AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up; // (0,1,0)
            _audioSource.Play(); //Play sfx
        }

        var keyLock = collision.GetComponent<KeyLock>();
        if (keyLock != null && keyLock == _keyLock)
        {
            keyLock.Unlock(); //Run public Unlock() in KeyLock.cs
            Destroy(gameObject); //Destroy our key after use
        }
    }
}
