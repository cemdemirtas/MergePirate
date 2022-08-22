using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem_Collider : MonoBehaviour
{
    private Unit _unit;
    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _unit.SetTarget(enemy);
        }
    }
}
