using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float _value;
    [Tooltip("Количество атак в секунду.")]
    [SerializeField] private float _speed;
    [Tooltip("Множитель атаки в процентах.")]
    [SerializeField] private float _valueMultiplier;

    public float Speed => _speed;

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