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

    private Coroutine _currentStartedCoroutine;
    private float _previousHealth;

    public void TakeDamageOnClick()
    {
        _previousHealth = _healthBar.value;
        _currentStartedCoroutine = StartCoroutine(TakeDamage());
    }

    public void TakeHealthOnClick()
    {
        _previousHealth = _healthBar.value;
        _currentStartedCoroutine = StartCoroutine(TakeHealth());
    }

    public IEnumerator TakeDamage()
    {
        while (_healthBar.value > (_previousHealth - _damage) && _healthBar.value > _healthBar.minValue)
        {
            _healthBar.value -= _damage * Time.deltaTime;
            _healthBarFillImage.color = _gradientColor.Evaluate(_healthBar.normalizedValue);
            yield return null;
        }
        if(_currentStartedCoroutine != null)
            StopCoroutine(_currentStartedCoroutine);
    }

    public IEnumerator TakeHealth()
    {
        while (_healthBar.value < (_previousHealth + _damage) && _healthBar.value < _healthBar.maxValue)
        {
            _healthBar.value += _health * Time.deltaTime;
            _healthBarFillImage.color = _gradientColor.Evaluate(_healthBar.normalizedValue);
            yield return null;
        }
        if (_currentStartedCoroutine != null)
            StopCoroutine(_currentStartedCoroutine);
    }
}
