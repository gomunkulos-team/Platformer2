using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _startValue;
    [SerializeField] private float _maxValue;
    [SerializeField] private float _minValue;

    private float _currentValue;
    private float _valueToTrigerDeath = 0;

    public event Action<float> ValueChanged;
    public event Action IsOver;

    public float MaxValue { get { return _maxValue; } private set { } }
    public float MinValue { get {return _minValue; } private set { } }
    public float Value { get {  return _currentValue; } private set { } }
    public float ValueToTriggerDeath { get { return _valueToTrigerDeath; } private set { } }

    private void Awake()
    {
        _currentValue = _startValue;
        ValueChanged?.Invoke(_currentValue);
    }

    public void Decrease(float value)
    {
        if (_currentValue <= _minValue)
            return;

        _currentValue -= value;

        if (_currentValue < _minValue)
            _currentValue = _minValue;

        ValueChanged?.Invoke(_currentValue);

        if (_currentValue <= _valueToTrigerDeath)
            IsOver?.Invoke();
    }

    public void Increse(float value)
    { 
        if (_currentValue >= _maxValue) 
            return;

        _currentValue += value;

        if(_currentValue > _maxValue)
            _currentValue = _maxValue;

        ValueChanged?.Invoke(_currentValue);
    }
}
