using System;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public event Action<Player> TouchPlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
            TouchPlayer?.Invoke(player);
    }
}