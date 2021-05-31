using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCounter : Counter
{
    [SerializeField] private Damager _damager;

    private void OnEnable()
    {
        _damager.HitCounterChanged += OnValueCounterChanged;
    }

    private void OnDisable()
    {
        _damager.HitCounterChanged -= OnValueCounterChanged;
    }
}
