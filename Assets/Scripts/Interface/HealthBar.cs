using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _valueNumber;

    [SerializeField] private Health _unitHealth;

    private void Awake()
    {
        _slider.minValue = _unitHealth.MinValue;
        _slider.maxValue = _unitHealth.MaxValue;
    }

    private void OnEnable()
    {
        _unitHealth.ValueChanged += Draw;
    }

    private void OnDisable()
    {
        _unitHealth.ValueChanged -= Draw;
    }

    private void Draw(float value)
    {
        _slider.value = value;
        _valueNumber.text = value.ToString();
    }
}
