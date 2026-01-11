using System.Collections;
using UnityEngine;

public class SmoothBar : BaseBar
{
    [SerializeField] private float _sliderSpeed = 1f;

    private float _targetHealth;

    private Coroutine _barUpdateCoroutine;

    private void Start()
    {
        if (Health != null && Slider != null)
        {
            _targetHealth = Health.CurrentHealth;
            Slider.value = _targetHealth;
        }
    }


    protected override void OnHealthChanged()
    {
        if (Health != null)
        {
            _targetHealth = Health.CurrentHealth;

            if (_barUpdateCoroutine != null)
            {
                StopCoroutine(_barUpdateCoroutine);
            }

            _barUpdateCoroutine = StartCoroutine(SmoothMoveSlider());
        }
    }

    private IEnumerator SmoothMoveSlider()
    {
        float nearZeroValue = 0.0001f;

        while (Mathf.Abs(Slider.value - _targetHealth) > nearZeroValue)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, _targetHealth, _sliderSpeed * Time.deltaTime * Health.Max);

            yield return null;
        }
    }
}