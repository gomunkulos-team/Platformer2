using System;
using System.Collections;
using UnityEngine;

public class VampiricAuraDamager : MonoBehaviour
{
    [SerializeField] private float _radius;

    private float _damage = 4;

    private float _timeIntervalForDamage = 0.75f;
    private Coroutine _coroutine;

    public event Action<float> DrainIsSuccessful;

    public float Damage { get; private set; }
    public float Radius { get; private set; }

    private void Awake()
    {
        Damage = _damage;
        Radius = _radius;
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Drain());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.azure;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private Enemy GetEnemy()
    {
        float minDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                float distance = (transform.position - enemy.transform.position).sqrMagnitude;

                if (distance < minDistance)
                {
                    nearestEnemy = enemy;
                    minDistance = distance;
                }
            }
        }

        return nearestEnemy;
    }

    private IEnumerator Drain()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeIntervalForDamage);

        while (true)
        {
            Enemy enemy = null;

            enemy = GetEnemy();

            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
                DrainIsSuccessful?.Invoke(_damage);
            }

            yield return wait;
        }
    }
}