using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Generator : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private int _numberEnemy;
    [SerializeField] private Player _player;

    private int _currentNumberEnemies = 0;
    private int _killedEnemies = 0;

    public event UnityAction<int> EnemyCounterChanged;

    private void Start()
    {
        EnemyCounterChanged?.Invoke(_killedEnemies);
        for (int i = 0; i < _numberEnemy; i++)
        {
            CreateEnemy();
        }
    }

    private void Update()
    {
        if(_currentNumberEnemies < _numberEnemy)
        {
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemies[Random.Range(0, _enemies.Length)], _spawnPoint[Random.Range(0, _spawnPoint.Length)].position, Quaternion.identity);
        enemy.Dead += OnDead;

        EnemyMover enemyMover = enemy.GetComponent<EnemyMover>();
        enemyMover.DefineTarget(_player);

        _currentNumberEnemies++;
    }

    private void OnDead(Enemy enemy)
    {
        _currentNumberEnemies--;
        _numberEnemy++;
        _killedEnemies++;
        EnemyCounterChanged?.Invoke(_killedEnemies);
        enemy.Dead -= OnDead;
    }
}
