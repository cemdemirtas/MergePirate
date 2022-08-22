using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> enemyList = new List<Enemy>();

    public static List<Enemy> GetEnemyList()
    {
        return enemyList;
    }

    public static Enemy GetClosestEnemy(Vector3 position, float maxRange)
    {
        float minDistance = 10000;
        Enemy _tempEnemy = null;
        foreach (var enemy in enemyList)
        {
            float distanceToEnemy = Vector3.Distance(position, enemy.transform.position);
            if ( distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                _tempEnemy = enemy;
            }
        }

        return _tempEnemy;
    }

    private void Awake()
    {
        enemyList.Add(this);
        
    }
    //private void Heal
}
