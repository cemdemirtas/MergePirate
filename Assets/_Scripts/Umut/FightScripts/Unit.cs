using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Enemy targetEnemy;

    public void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }
}
