using System;
using UnityEngine;

public class TriggerCollector : MonoBehaviour
{
    public event Action<Coin> CoinTouched;
    public event Action<FirstAid> FirstAidTouched;
    public event Action<ManaCrystal> ManaCrystalTouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.TryGetComponent(out Coin coin))
        //    CoinTouched?.Invoke(coin);
        //else if (collision.TryGetComponent(out FirstAid firstAid))
        //    FirstAidTouched?.Invoke(firstAid);
        //else if (collision.TryGetComponent(out ManaCrystal crystal))
        //    ManaCrystalTouched?.Invoke(crystal);

        //CoinTouched.Invoke(collision.GetComponent<Coin>());

        switch (collision.GetType().Name)
        {
            case Items.Coin:
                CoinTouched?.Invoke(collision.GetComponent<Coin>());
                break;

            case Items.FirstAid:
                FirstAidTouched?.Invoke(collision.GetComponent<FirstAid>());
                break;

            case Items.ManaCrystal:
                ManaCrystalTouched?.Invoke(collision.GetComponent<ManaCrystal>());
                break;
        }
    }
}
