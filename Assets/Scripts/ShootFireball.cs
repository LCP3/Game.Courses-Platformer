using UnityEngine;

public class ShootFireball : MonoBehaviour
{
    [SerializeField] Fireball _fireballPrefab;
    [SerializeField] float _fireRate = 0.25f;

    Player _player;
    string _fireButton;
    float _nextFireTime;
    string _horizontalAxis;
    public static int _fireballCount { get; set; }

    void Awake()
    {
        _player = GetComponent<Player>();
        _fireButton = $"P{_player.PlayerNumber}Fire";
        _horizontalAxis = $"P{_player.PlayerNumber}Horizontal";
    }

    void Update()
    {
        if (Input.GetButtonDown(_fireButton) && _fireballCount < 3 && Time.time >= _nextFireTime)
            //if (Input.GetButtonDown(_fireButton) && Time.time >= _nextFireTime)
            {
            var horizontal = Input.GetAxis(_horizontalAxis);
            Fireball fireball = Instantiate(_fireballPrefab, transform.position, Quaternion.identity); //Spawn a fireball
            fireball.Direction = horizontal >= 0 ? 1f : -1f; //Shoot a fireball in the direction the player is facing
            _nextFireTime = Time.time + _fireRate; //Set the delay between shots
            _fireballCount++; //Add to the counter, subracted on destroy in Fireball.cs
        }
    }
}
