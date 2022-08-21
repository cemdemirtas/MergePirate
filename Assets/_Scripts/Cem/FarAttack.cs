using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FarAttack : MonoBehaviour
{
    [SerializeField] UnitSO unitSO;
    GameObject target;
    private Animator animator;

    GameObject[] allEnemy;

    private float CloseTarget;
    private float distanceToTarget;
    private float _attackTime;

    BulletPool BulletPoolController;
    private int BulletLine;
    private GameObject Bullet;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _attackTime = unitSO.unitAttackSpeed;
        BulletPoolController = GameObject.FindObjectOfType<BulletPool>();

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
        if (!target.gameObject.activeInHierarchy)
        {
            return;
        }
        if (Bullet.gameObject.activeInHierarchy)
        {
            BulletLine++;
            Bullet = BulletPoolController.BulletPoolList[BulletLine].transform.gameObject;
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
        if (allEnemy.Length <= 0)
        {
            animator.SetBool("Win", true);
            //animator.SetBool("Attack", false);
            animator.SetBool("Idle", false);
            Debug.Log("finish");

        }
        for (int i = 0; i < allEnemy.Length; i++)
        {
            distanceToTarget = (allEnemy[i].transform.position - gameObject.transform.position).sqrMagnitude;
            if (distanceToTarget < CloseTarget)
            {
                CloseTarget = distanceToTarget;
                target = allEnemy[i];

            }
        }
        animator.SetBool("Attack", true);

        transform.LookAt(target.transform);
    }
}
