using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Firewall : MonoBehaviour
{
    [Tooltip("2 is fast, and 0.2 is slow, and a positive value will rotate the firewall clockwise.")]
    [SerializeField] private float _rotationSpeed = 1f;
    private Vector3 _rotationVelocity;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rotationVelocity = new Vector3(0, 0, _rotationSpeed);
    }
    private void FixedUpdate()
    {
        //_rb.MoveRotation( Quaternion.Euler(_rotationVelocity * Time.fixedDeltaTime) );
        _rb.rotation += _rotationSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name != "Player") return;

        GameInfo.PlayerDies.Invoke();
    }
}
