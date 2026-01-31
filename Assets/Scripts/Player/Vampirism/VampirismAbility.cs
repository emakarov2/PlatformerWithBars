using System;
using System.Collections;
using UnityEngine;

public class VampirismAbility : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;
    [SerializeField] private float _damagePerSecond = 7f;

    [Header("Dependences")]
    [SerializeField] private VictimDetector _detector;
    [SerializeField] private Player _player;
    [SerializeField] private VampirUI _userInterface;

    private bool _isActive = false;
    private bool _isCooldowning = false;

    private Coroutine _activityCoroutine;

    private Entity _currentVictim;

    public event Action AbilitySwitched;

    private void Start()
    {
        if (_userInterface != null)
        {
            _userInterface.Hide();
        }
    }

    public void ActivateIfAvailable()
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

        AbilitySwitched?.Invoke();

        if (_userInterface != null)
        {
            _userInterface.Show();
            _userInterface.OnAbilityStarted(_duration);
        }

        _activityCoroutine = StartCoroutine(VampiringCoroutine());
    }

    private void Deactivate()
    {
        _isActive = false;
        _currentVictim = null;

        AbilitySwitched?.Invoke();

        if (_activityCoroutine != null)
        {
            StopCoroutine(_activityCoroutine);
            _activityCoroutine = null;
        }

        StartCoroutine(CooldowningVampirismCoroutine());
    }

    private IEnumerator VampiringCoroutine()
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

    private IEnumerator CooldowningVampirismCoroutine()
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