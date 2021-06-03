using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageCounter;
    [SerializeField] private float _timeShow;

    private float _currentTime;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        ChechTimeShow();
    }

    private void ChechTimeShow()
    {
        if (_currentTime >= _timeShow)
        {
            ClearCounter();          
        }
    }

    private void ClearCounter()
    {
        _damageCounter.text = "";
    }

    public void ShowDamage(float damage)
    {
        _damageCounter.text = "-" + damage.ToString();
        _currentTime = 0;
    }
}
