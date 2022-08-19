using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FarEnemyAttack : MonoBehaviour
{
    [SerializeField] UnitSO unitSO;
    GameObject target;

    GameObject[] allEnemy;

    private float ClosestTarget;
    private float distanceToTarget;
    private float _attackTime;

    private int enemyBallLine;
    private GameObject enemyBall;
    EnemyBulletPool EnemyBulletPool;
    private void Start()
    {
    }

    private void Awake()
    {
        EnemyBulletPool = GameObject.FindObjectOfType<EnemyBulletPool>();
        _attackTime = unitSO.unitAttackSpeed;
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

        if (!target.gameObject.activeInHierarchy)
        {
            return;
        }
        enemyBall = EnemyBulletPool.enemyBulletList[enemyBallLine].transform.gameObject;
        if (enemyBall.gameObject.activeInHierarchy)
        {
            enemyBallLine++;
            enemyBall = EnemyBulletPool.enemyBulletList[enemyBallLine].transform.gameObject;
        }
        enemyBall.transform.position = transform.GetChild(1).transform.position;
        enemyBall.transform.gameObject.SetActive(true);
        EnemyBulletPool.enemyBulletList[enemyBallLine].transform.DOMove(target.transform.position, 0.5f);
        enemyBallLine++;
        if (EnemyBulletPool.enemyBulletList.Count == enemyBallLine)
        {
            enemyBallLine = 0;
        }
    }

    void findNearEnemy()
    {
        ClosestTarget = Mathf.Infinity;
        allEnemy = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < allEnemy.Length; i++)
        {
            distanceToTarget = (allEnemy[i].transform.position - gameObject.transform.position).sqrMagnitude;
            if (distanceToTarget < ClosestTarget)
            {
                ClosestTarget = distanceToTarget;
                target = allEnemy[i];
            }
        }
        transform.LookAt(target.transform);
    }
}
