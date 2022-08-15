using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    public List<GameObject> enemyBulletList;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyBulletList.Add(transform.GetChild(i).transform.gameObject);
        }
    }

    public void EnemyBulletAddList(GameObject _gameObject)
    {
        _gameObject.SetActive(false);
        enemyBulletList.Add(_gameObject);
    }
}
