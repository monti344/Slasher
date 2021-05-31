using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _energi;
    [SerializeField] private float _energyRecoveryRate;
    [SerializeField] private float _recoveredEnergy;

    private float _currentHealth;
    private float _currentEnergi;
    private float _curentTime;

    public float CurrentEnergi => _currentEnergi;

    public event UnityAction<float, float> HealthChanged;
    public event UnityAction<float, float> EnergiChanged;
    public event UnityAction Dead;

    private void Start()
    {
        _currentHealth = _health;
        _currentEnergi = _energi;
    }

    private void Update()
    {
        RestoreEnergy();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0)
        {
            Dead?.Invoke();
        }
        HealthChanged?.Invoke(_currentHealth, _health);
    }

    public void SpendEnergy(float energi)
    {
        _currentEnergi -= energi;
        EnergiChanged?.Invoke(_currentEnergi, _energi);
    }

    private void RestoreEnergy()
    {
        _curentTime += Time.deltaTime;
        if (_curentTime >= _energyRecoveryRate)
        {
            if(_recoveredEnergy <= _energi - _currentEnergi)
            {
                _currentEnergi += _recoveredEnergy;
            }
            else
            {
                _currentEnergi += _energi - _currentEnergi;
            }
            EnergiChanged?.Invoke(_currentEnergi, _energi);
            _curentTime = 0;
        }
    }
}
