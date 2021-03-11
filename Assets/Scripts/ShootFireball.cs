using UnityEngine;

public class ShootFireball : MonoBehaviour
{
    [SerializeField] Fireball _fireballPrefab;
    Player _player;
    string _fireButton;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _fireButton = $"P{_player.PlayerNumber}Fire";
    }

    void Update()
    {
        if (Input.GetButtonDown(_fireButton))
            Instantiate(_fireballPrefab, transform.position, Quaternion.identity);

    }
}
