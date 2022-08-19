using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FarAttack : MonoBehaviour
{
    [SerializeField] UnitSO unitSO;
    GameObject target;
    GameObject[] allEnemy;

    private float CloseTarget;
    private float distanceToTarget;
    private float _attackTime;

    BulletPool BulletPoolController;
    private int BulletLine;
    private GameObject Bullet;

    private void Awake()
    {
        _attackTime = unitSO.unitAttackSpeed;
        BulletPoolController = GameObject.FindObjectOfType<BulletPool>();

    }


    private void OnEnable()
    {
        OnGame = true;
    }

    private void Update()
    {
        if (GameManager.Instance.GameOn)
        {
            _attackTime -= Time.deltaTime;
            switch (_attackTime)
            {
                case <= 0:
                    findNearEnemy();
                    Attack();
                    _attackTime = unitSO.unitAttackSpeed;
                    break;
            }
        }
    }
    void Attack()
    {
        Bullet = BulletPoolController.BulletPoolList[BulletLine].transform.gameObject;
        if (Bullet.gameObject.activeInHierarchy)
        {
            BulletLine++;
            Bullet = BulletPoolController.BulletPoolList[BulletLine].transform.gameObject;
        }
        if (!target.gameObject.activeInHierarchy)
        {
            return;
        }
        Bullet.transform.position = transform.GetChild(0).transform.position;
        Bullet.transform.gameObject.SetActive(true);
        BulletPoolController.BulletPoolList[BulletLine].transform.DOMove(target.transform.position, 0.5f);
        BulletLine++;
        if (BulletPoolController.BulletPoolList.Count == BulletLine)
        {
            BulletLine = 0;
        }
    }

    void findNearEnemy()
    {
        CloseTarget = Mathf.Infinity;
        allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < allEnemy.Length; i++)
        {
            distanceToTarget = (allEnemy[i].transform.position - gameObject.transform.position).sqrMagnitude;
            if (distanceToTarget < CloseTarget)
            {
                CloseTarget = distanceToTarget;
                target = allEnemy[i];

            }
        }
        transform.LookAt(target.transform);
    }
}