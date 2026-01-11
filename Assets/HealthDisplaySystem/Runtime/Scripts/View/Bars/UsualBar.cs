using UnityEngine.UI;

public class UsualBar : BaseBar
{
    private void Start()
    {
        if (Slider != null && Health != null )
        {
            Slider.value = Health.CurrentHealth;
        }
    }

    protected override void OnHealthChanged()
    {
        if (Slider != null && Health != null)
        {
            Slider.value = Health.CurrentHealth;
        }
    }
}
