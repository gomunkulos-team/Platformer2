using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampiricAuraBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fillImage;
    [SerializeField] private VampiricAura _aura;

    private float _minSliderValue = 0;
    private float _maxSliderValue = 1;
    private float _timeInterval = 0.5f;
    private float _timeDeviation = 0.01f;

    private WaitForSeconds _wait;

    private Color _colorForRecharge = Color.gray;
    private Color _colorForActive = Color.green;

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeInterval);

        _slider.minValue = _minSliderValue;
        _slider.maxValue = _maxSliderValue;
    }

    private void OnEnable()
    {
        _fillImage.color = _colorForActive;
        _slider.value = _maxSliderValue;

        _aura.ActivateDrain += ActivateDrain;
        _aura.RechargeDrain += ActivateRecharge;
    }

    private void OnDisable()
    {
        _aura.ActivateDrain -= ActivateDrain;
        _aura.RechargeDrain -= ActivateRecharge;
    }

    private void ActivateDrain(float time)
    {
        StartCoroutine(DecreaseBar(time));
    }

    private void ActivateRecharge(float time)
    {
        StartCoroutine(IncreaseBar(time));
    }

    private IEnumerator DecreaseBar(float targetTime)
    {
        float currentTime = 0;

        while (Mathf.Abs(targetTime - currentTime) > _timeDeviation)
        {
            _slider.value = (targetTime - currentTime) / targetTime;
            yield return _wait;
            currentTime += _timeInterval;
        }

        _fillImage.color = _colorForRecharge;
    }

    private IEnumerator IncreaseBar(float targetTime)
    {
        float currentTime = 0;

        while (Mathf.Abs(targetTime - currentTime) > _timeDeviation)
        {
            _slider.value = currentTime / targetTime;
            yield return _wait;
            currentTime += _timeInterval;
        }

        _fillImage.color = _colorForActive;
    }
}