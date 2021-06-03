using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _swordSlash;
    [SerializeField] private ParticleSystem _soul;
    [SerializeField] private float _damage;
    [SerializeField] private float _health;
    [SerializeField] private float _cooldown;
    [SerializeField] private DamageNumber _damageNumber;

    private float _currentTime;

    public event UnityAction<Enemy> Dead;

    private void Damage(Player player)
    {
        _swordSlash.Play();
        player.TakeDamage(_damage);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _cooldown)
            {
                Damage(player);
                _currentTime = 0;
            }
        }      
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _damageNumber.ShowDamage(damage);

        if(_health <= 0)
        {
            Instantiate(_soul, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Dead?.Invoke(this);
        }
    }
}
