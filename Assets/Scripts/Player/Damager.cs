using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class Damager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _swordSlashesLight;
    [SerializeField] private ParticleSystem _swordSlashHeavy;
    [SerializeField] private GameObject _hitPanel;
    [SerializeField] private float _seriesTime;
    [SerializeField] private float _damageLight;
    [SerializeField] private float _damageHeavy;
    [SerializeField] private float _energyCost;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _reboundForce;
    [SerializeField] private float _comboTime;

    private float _rechargingTime;
    private float _currentComboTime;
    private float _currentSeriesTime;
    private Player _player;
    private int _hitCounter = 0;
    private float _startSeriesTime;

    private bool _isRecharged => _rechargingTime >= _cooldown;
    private bool _isComboActive => _currentComboTime <= _comboTime;

    public event UnityAction<int> HitCounterChanged;

    private void Start()
    {
        _player = GetComponent<Player>();
        _startSeriesTime = _seriesTime;
    }

    private void Update()
    {
        CheckSeriesTimer();
        _rechargingTime += Time.deltaTime;
        _currentComboTime += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Enemy enemy))
        {
            Attack(enemy);
        }
    }

    private void Attack(Enemy enemy)
    {
        if (Input.GetMouseButtonDown(0) && _isRecharged)
        {
            LightAttack(enemy);
            ResetTime();
        }
        if(Input.GetMouseButtonDown(1) && _isComboActive)
        {
            HeavyAttack(enemy);
            ResetTime();
        }
    }

    private void LightAttack(Enemy enemy)
    {
        _swordSlashesLight[Random.Range(0, _swordSlashesLight.Length)].Play();
        enemy.TakeDamage(_damageLight);
        ActivateSeries();
    }

    private void HeavyAttack(Enemy enemy)
    {
        if(_player.CheckEnergi(_energyCost))
        {
            _swordSlashHeavy.Play();
            _player.SpendEnergy(_energyCost);
            enemy.TakeDamage(_damageHeavy);
            EnemyMover enemyMover = enemy.GetComponent<EnemyMover>();
            enemyMover.FlyAway(_reboundForce, _player);
            ActivateSeries();
        }
    }

    private void ActivateSeries()
    {
        _hitPanel.SetActive(true);
        _hitCounter++;
        _seriesTime += _startSeriesTime;
        HitCounterChanged?.Invoke(_hitCounter);
    }

    private void CheckSeriesTimer()
    {
        _currentSeriesTime += Time.deltaTime;
        if (_currentSeriesTime >= _seriesTime)
        {
            _hitCounter = 0;
            _currentSeriesTime = 0;
            _seriesTime = _startSeriesTime;
            HitCounterChanged?.Invoke(_hitCounter);
            _hitPanel.SetActive(false);
        }
    }

    private void ResetTime()
    {
        _rechargingTime = 0;
        _currentComboTime = 0;
    }
}
