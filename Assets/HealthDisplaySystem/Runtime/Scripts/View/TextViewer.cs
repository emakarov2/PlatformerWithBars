using UnityEngine;
using TMPro;

public class TextViewer : HealthViewer
{
    [SerializeField] private TMP_Text _text;
   
    private void Start()
    {
        OnHealthChanged();
    }    

    protected override void OnHealthChanged()
    {
        if (_text != null)
        {
            _text.text = $"{Health.CurrentHealth}/{Health.Max}";
        }
    }
}
