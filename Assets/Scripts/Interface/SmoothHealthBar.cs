using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _timeToChange ;
    [SerializeField] private float _deltaToChangeSlider;
    [SerializeField] private Slider _slider;

    private float _minSliderValue = 0;
    private float _maxSliderValue = 1;
    private float _previoseHealthValue;
    private float _timeDeviation = 0.01f;

    private void Awake()
    {
        _slider.minValue = _minSliderValue;
        _slider.maxValue = _maxSliderValue;
    }

    private void OnEnable()
    {
        _text.text = _health.Value.ToString() + "/" + _health.MaxValue;

        _health.ValueChanged += Draw;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= Draw;
    }

    private void Start()
    {
        _slider.value = _health.Value / _health.MaxValue;
        Debug.Log(_slider.value);
        _previoseHealthValue = _slider.value;
    }

    private void Draw(float value)
    {
        float targetValue = value / _health.MaxValue;
        float delta = targetValue * _deltaToChangeSlider;

        _text.text = _health.Value.ToString() + "/" + _health.MaxValue;

        StartCoroutine(StartChangeValue(targetValue, delta));
    }

    private IEnumerator StartChangeValue(float targetValue, float delta)
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(_timeToChange);

        while (Mathf.Abs(_slider.value - targetValue) > _timeDeviation)
        {
            yield return wait;
            _slider.value = Mathf.MoveTowards(_previoseHealthValue, targetValue, delta);
            _previoseHealthValue = _slider.value;
        }
    }
}