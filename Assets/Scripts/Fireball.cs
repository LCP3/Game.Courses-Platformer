using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float _launchForce = 5;
    [SerializeField] float _bounceForce = 5;


    int _bouncesRemaining = 3;
    Rigidbody2D _rigidbody;

    public float Direction { get; internal set; }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(_launchForce * Direction, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ITakeDamage ableToDamage = collision.collider.GetComponent<ITakeDamage>();
        if (ableToDamage != null)
        {
            ableToDamage.TakeDamage();
            ShootFireball._fireballCount--;
            Destroy(gameObject);
            return;
        }

        _bouncesRemaining--;
        if (_bouncesRemaining < 0)
        {
            ShootFireball._fireballCount--; //Decrease the count of fireballs
            Destroy(gameObject); // Destroy the created object
        }
        else
            _rigidbody.velocity = new Vector2(_launchForce * Direction, _bounceForce); //Maintain velocity
    }
}
