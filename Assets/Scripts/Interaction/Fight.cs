using System;
using System.Collections;
using UnityEngine;

public class Fight : MonoBehaviour
{
    private Atack _atack;
    private Coroutine _attackCoroutine;

    public event Action<Player> TargetPlayer;

    private void Awake()
    {
        _atack = GetComponent<Atack>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _attackCoroutine = StartCoroutine(StartAttack(player));
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
            StopCoroutine(_attackCoroutine);
    }

    private IEnumerator StartAttack(Player player)
    {
        float waitTime = 1 / _atack.Speed;

        while (true)
        {
            TargetPlayer?.Invoke(player);
            yield return new WaitForSecondsRealtime(waitTime);
        }
    }
}