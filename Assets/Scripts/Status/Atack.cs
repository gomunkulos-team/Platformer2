using UnityEngine;

public class Atack : MonoBehaviour
{
    [SerializeField] private float _value;
    [Tooltip("Множитель атаки в процентах.")]
    [SerializeField] private float _valueMultiplier;

    public void IncreaseValue(float value)
    {
        _value += value;
    }

    public void IncreaseMultuplier(float multiplier)
    {
        _valueMultiplier += multiplier;
    }

    public float GetValue()
    {
        return _value * (_valueMultiplier / 100);
    }
}