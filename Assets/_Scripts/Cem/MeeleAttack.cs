using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MeeleAttack : MonoBehaviour
{
    [SerializeField] UnitSO unitSO;

    CharacterController characterController;

    private GameObject target;

    private Animator animator;

    private float attackTime;
    private float distanceToClosestTarget;
    private float distanceToTarget;
    private float SmoothSpeed=0.5f;
    GameObject[] allEnemy;

    private bool walk;

    bool attack;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        attackTime = 0;
        walk = true;
        //animator.SetTrigger("Walk");
    }


    private void Update()
    {
        if (attack)
        {
            AttackTheEnemy();
            attackTime -= Time.deltaTime;
            walk = false;
            GameManager.Instance.GameOn = false;
        }
        else
        {
            walk = true;
            //GameManager.Instance.GameOn = true;
        }
        if (/*unitSO.startGame &&*/ GameManager.Instance.GameOn == true)
        {
            findNearEnemy();
            transform.Translate(transform.forward * Time.deltaTime * unitSO.unitSpeed * SmoothSpeed);
            //transform.DOMove(target.transform.position, 3f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Enemy"))
        {
            attack = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Enemy"))
        {
            attack = false;
        }
    }
    void findNearEnemy()
    {
        distanceToClosestTarget = Mathf.Infinity;
        allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (allEnemy.Length == 0)
        {
            //unitSO.startGame = false;
            GameManager.Instance.GameOn = false;
        }
        for (int i = 0; i < allEnemy.Length; i++)
        {
            distanceToTarget = (allEnemy[i].transform.position - gameObject.transform.position).sqrMagnitude;
            if (distanceToTarget < distanceToClosestTarget)
            {
                distanceToClosestTarget = distanceToTarget;
                target = allEnemy[i];
            }
        }
        transform.LookAt(target.transform);
    }

    void AttackTheEnemy()
    {
        if (!target.activeInHierarchy)
        {
            attack = false;
            return;
        }
        switch (attackTime)
        {
            case <= 0:
                target.GetComponent<EnemyController>().TakeDamage(characterController.characterLevel * 10f);
                attackTime = unitSO.unitAttackSpeed;
                //animator.SetTrigger("Attack");
                transform.LookAt(target.transform);
                break;
        }

    }
}
