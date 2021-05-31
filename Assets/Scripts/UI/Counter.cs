using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Counter : MonoBehaviour
{
    [SerializeField] private TMP_Text _number;

    public void OnValueCounterChanged(int number)
    {
        _number.text = number.ToString();
    }
}
