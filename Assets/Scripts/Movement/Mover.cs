using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;

    private Rigidbody2D _rigidbody;

    public float Speed { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Speed = _speed;
    }

    public void MoveOnX(float direction)
    {
        _rigidbody.linearVelocity = new Vector2(_speed * direction, _rigidbody.linearVelocityY);
    }

    public void Jump()
    {
        _rigidbody.linearVelocityY = 0;
        _rigidbody.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);
    }
}
