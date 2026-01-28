using System.Collections;
using UnityEngine;

public class Vampir : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;
    [SerializeField] private float _damagePerSecond = 7f;

    [Header("Dependences")]
    [SerializeField] private VictimDetector _detector;
    [SerializeField] private Player _player;
    [SerializeField] private VampirVisualizer _visualizer;
    [SerializeField] private VampirUI _userInterface;

    private bool _isActive = false;
    private bool _isCooldowning = false;

    private Coroutine _activityCoroutine;

    private Entity _currentVictim;

    private void Start()
    {
        if (_userInterface != null)
        {
            _userInterface.Hide();
        }
    }

    public void TryActivate()
    {
        if (_isActive == false && _isCooldowning == false)
        {
            Activate();
        }
    }

    private void Activate()
    {
        _isActive = true;
        _currentVictim = null;

        if (_visualizer != null)
        {
            _visualizer.StartAbility();
        }

        if (_userInterface != null)
        {
            _userInterface.Show();
            _userInterface.OnAbilityStarted(_duration);
        }

        _activityCoroutine = StartCoroutine(AbilityCoroutine());
    }

    private void Deactivate()
    {
        _isActive = false;
        _currentVictim = null;

        if (_visualizer != null)
        {
            _visualizer.StopAbility();
        }

        if (_activityCoroutine != null)
        {
            StopCoroutine(_activityCoroutine);
            _activityCoroutine = null;
        }

        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator AbilityCoroutine()
    {
        float timer = _duration;

        while (timer > 0)
        {
            Entity nearestEnemy = _detector?.FindNearestAliveEnemy();
            _currentVictim = nearestEnemy;

            if (_currentVictim != null)
            {
                float stolenHealth = _damagePerSecond * Time.deltaTime;
                _currentVictim.AcceptAttack(stolenHealth);

                if (_player.TryGetComponent(out Health health))
                {
                    health.Increase(stolenHealth);
                }
            }

            if (_userInterface != null)
            {
                _userInterface.UpdateAbilityTimer(timer, _duration);
            }

            timer -= Time.deltaTime;

            yield return null;
        }

        Deactivate();
    }

    private IEnumerator CooldownCoroutine()
    {
        _isCooldowning = true;
        float timer = _cooldown;

        while (timer > 0)
        {
            if (_userInterface != null)
            {
                _userInterface.UpdateCooldownTimer(timer, _cooldown);
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        _isCooldowning = false;

        if (_userInterface != null)
        {
            _userInterface.OnAbilityReady();
        }
    }
}
