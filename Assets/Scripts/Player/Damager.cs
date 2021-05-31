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
    [SerializeField] private float _comboTime;
    [SerializeField] private float _damageLight;
    [SerializeField] private float _damageHeavy;
    [SerializeField] private float _energyCost;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _reboundForce;

    private float _rechargingTime;
    private float _currentComboTime;
    private Player _player;
    private int _hitCounter = 0;
    private float _startComboTime;

    public event UnityAction<int> HitCounterChanged;

    private void Start()
    {
        _player = GetComponent<Player>();
        _startComboTime = _comboTime;
    }

    private void Update()
    {
        CheckComboTimer();
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
        _rechargingTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && _rechargingTime >= _cooldown)
        {
            _rechargingTime = 0;
            LightAttack(enemy);
        }
        if (Input.GetMouseButtonDown(1) && _rechargingTime >= _cooldown)
        {
            _rechargingTime = 0;
            HeavyAttack(enemy);
        }
    }

    private void LightAttack(Enemy enemy)
    {
        _swordSlashesLight[Random.Range(0, _swordSlashesLight.Length)].Play();
        enemy.TakeDamage(_damageLight);
        ActivateCombo();
    }

    private void HeavyAttack(Enemy enemy)
    {
        if(_player.CurrentEnergi >= _energyCost)
        {
            _swordSlashHeavy.Play();
            _player.SpendEnergy(_energyCost);
            enemy.TakeDamage(_damageHeavy);
            EnemyMover enemyMover = enemy.GetComponent<EnemyMover>();
            enemyMover.FlyAway(_reboundForce, _player);
            ActivateCombo();
        }
    }

    private void ActivateCombo()
    {
        _hitPanel.SetActive(true);
        _hitCounter++;
        _comboTime += _startComboTime;
        HitCounterChanged?.Invoke(_hitCounter);
    }

    private void CheckComboTimer()
    {
        _currentComboTime += Time.deltaTime;
        if (_currentComboTime >= _comboTime)
        {
            _hitCounter = 0;
            _currentComboTime = 0;
            _comboTime = _startComboTime;
            HitCounterChanged?.Invoke(_hitCounter);
            _hitPanel.SetActive(false);
        }
    }
}
