using UnityEngine;

public class VampirVisualizer : MonoBehaviour
{
    [SerializeField] private VictimDetector _detector;
    [SerializeField] private SpriteRenderer _area;

    private float _multipleRadiusToDiameter = 2f;
    private float _ThirtyPercent = 0.1f;

    private void Start()
    {
        if (_area != null)
        {
            _area.enabled = false;
            UpdateAreaScale();

            Color color = _area.color;
            color.a = _ThirtyPercent;
            _area.color = color;
        }
    }

    public void StartAbility()
    {
        if (_area != null)
        {
            _area.enabled = true;
        }
    }

    public void StopAbility()
    {
        if (_area != null)
        {
            _area.enabled = false;
        }
    }

    private void UpdateAreaScale()
    {
        if (_area != null && _detector != null)
        {
            float scale = _detector.Radius * _multipleRadiusToDiameter;
            _area.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
