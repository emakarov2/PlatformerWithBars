using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VampirUI : MonoBehaviour
{
    [SerializeField] private Slider _ability;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _status;

    private Coroutine _hideCoroutine;

    private void OnDestroy()
    {
        StopHideCoroutine();
    }

    public void Show()
    {
        StopHideCoroutine();      

        if (_ability != null)
        {
            _ability.gameObject.SetActive(true);
        }

        if (_status != null)
        {
            _status.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        StopHideCoroutine();
        
        if (_ability != null)
        {
            _ability.gameObject?.SetActive(false);
        }

        if (_status != null)
        {
            _status.gameObject.SetActive(false);
        }
    }

    public void OnAbilityStarted(float duration)
    {
        if (_ability == null)
        { 
            return;
        }

        StopHideCoroutine();

        _ability.maxValue = duration;
        _ability.value = duration;

        UpdateTimerText(duration.ToString("F0") + "s");
        UpdateStatusText("Vampiring", Color.red);
    }

    public void UpdateAbilityTimer(float currentTime, float maxTime)
    {
        if (_ability == null) 
        {
            return;
        }

        _ability.maxValue = maxTime;
        _ability.value = currentTime;

        UpdateTimerText(currentTime.ToString("F1") + "s");
    }

    public void UpdateCooldownTimer(float currentTime, float maxTime)
    {
        if(_ability == null)
        {
            return;
        }

        _ability.maxValue = maxTime; 
        _ability.value = maxTime - currentTime;

        UpdateTimerText(currentTime.ToString("F1") + "s");
        UpdateStatusText("Cooldown", Color.blue);
    }

    public void OnAbilityReady()
    {
        float timeTillHide = 2f;
        
        if(_ability == null)
        { return;}

        StopHideCoroutine();

        _ability.value = _ability.maxValue;
        UpdateTimerText("");
        UpdateStatusText("Ready", Color.green);         

        _hideCoroutine = StartCoroutine(HideAfterDelayCoroutine(timeTillHide));
    }

    private void UpdateTimerText(string text)
    {
       if(_timer != null)
        {
            _timer.text = text;
        }
    }

    private void UpdateStatusText(string text, Color color)
    {
        if(_status != null)
        {
            _status.text = text;
            _status.color = color;
        }
    }

    private void StopHideCoroutine()
    {
        if(_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
            _hideCoroutine = null;
        }
    }

    private IEnumerator HideAfterDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        Hide();

        _hideCoroutine = null;
    }
}