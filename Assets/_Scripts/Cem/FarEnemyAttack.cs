using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FarEnemyAttack : MonoBehaviour
{
    [SerializeField] UnitSO unitSO;

    [SerializeField] bool OnGame;
    GameObject target;
    GameObject[] allEnemy;

    private float ClosestTarget;
    private float distanceToTarget;
    private float _attackTime;
    private Animator animator;

    private int enemyBallLine;
    private GameObject enemyBall;
    EnemyBulletPool EnemyBulletPool;


    private void Awake()
    {
        animator = GetComponent<Animator>();
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
                    animator.SetBool("Attack", true);
                    break;
            }
        }
    }
    void Attack()
    {

        if (!target.gameObject.activeInHierarchy)
        {
            animator.SetBool("Win", true);

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
        if (allEnemy.Length <= 0)
        {
            animator.SetBool("Win", true);
            //animator.SetBool("Attack", false);
            animator.SetBool("Idle", false);
            Debug.Log("Game Over");

        }
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
