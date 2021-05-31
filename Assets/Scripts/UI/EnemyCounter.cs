using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCounter : Counter
{
    [SerializeField] private Generator _generator;

    private void OnEnable()
    {
        _generator.EnemyCounterChanged += OnValueCounterChanged;
    }

    private void OnDisable()
    {
        _generator.EnemyCounterChanged -= OnValueCounterChanged;
    }
}
