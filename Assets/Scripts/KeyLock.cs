using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    [SerializeField] UnityEvent _onUnlocked;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    public void Unlock() 
    {
        _spriteRenderer.enabled = false;
        _collider2D.enabled = false;
        Debug.Log("Unlocked");
        _onUnlocked.Invoke();
    }
}
