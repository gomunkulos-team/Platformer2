using System;
using UnityEngine;

public class TriggerCollector : MonoBehaviour
{
    public event Action<Coin> CoinTouched;
    public event Action<FirstAid> FirstAidTouched;
    public event Action<ManaCrystal> ManaCrystalTouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            CoinTouched?.Invoke(coin);
        else if (collision.TryGetComponent(out FirstAid firstAid))
            FirstAidTouched?.Invoke(firstAid);
        else if (collision.TryGetComponent(out ManaCrystal crystal))
            ManaCrystalTouched?.Invoke(crystal);
    }
}
