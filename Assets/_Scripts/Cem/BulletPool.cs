using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public List<GameObject> BulletPoolList;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BulletPoolList.Add(transform.GetChild(i).transform.gameObject);
        }
    }

    public void BulletAddList(GameObject _gameObject)
    {
        _gameObject.SetActive(false);
        BulletPoolList.Add(_gameObject);
    }
}
