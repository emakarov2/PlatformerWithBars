using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBar : HealthViewer
{
    [SerializeField] protected Slider Slider;

    protected override void OnEnable()
    {
        base.OnEnable();

        if(Slider != null && Health != null)
        {
            Slider.minValue = 0f;
            Slider.maxValue = Health.Max;
        }
    }
}
