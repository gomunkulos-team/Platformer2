using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public event Action<Enemy> EnemyTouched;

    public bool IsGrounded {  get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Ground>(out _))
            IsGrounded = true;

        else if (collision.gameObject.TryGetComponent(out Enemy enemy))
            EnemyTouched?.Invoke(enemy);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
            IsGrounded = false;
    }
}
