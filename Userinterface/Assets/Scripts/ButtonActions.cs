using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _health;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image _healthBarFillImage;
    [SerializeField] private Gradient _gradientColor;
    [SerializeField] private float _actionDuration;

    private Coroutine _currentStartedCoroutine;

    public void TakeDamageOnClick()
    {
        _currentStartedCoroutine = StartCoroutine(ChangeHealth(_healthBar.value, _healthBar.value - _damage));
    }

    public void TakeHealthOnClick()
    {
        _currentStartedCoroutine = StartCoroutine(ChangeHealth(_healthBar.value, _healthBar.value + _health));
    }

    public IEnumerator ChangeHealth(float startValue, float endValue)
    {
        float elapsed = 0;
        float nextValue;

        while (elapsed < _actionDuration)
        {
            nextValue = Mathf.Lerp(startValue, endValue, elapsed / _actionDuration);
            FillHealthBar(nextValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        FillHealthBar(endValue);
        if (_currentStartedCoroutine != null)
            StopCoroutine(_currentStartedCoroutine);
    }

    public void FillHealthBar(float fillValue)
    {
        _healthBar.value = fillValue;
        _healthBarFillImage.color = _gradientColor.Evaluate(_healthBar.normalizedValue);
    }
}
