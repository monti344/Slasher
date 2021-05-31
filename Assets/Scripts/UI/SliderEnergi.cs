using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderEnergi : Bar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.EnergiChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.EnergiChanged -= OnValueChanged;
    }
}
