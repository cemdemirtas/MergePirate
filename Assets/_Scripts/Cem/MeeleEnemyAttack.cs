using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeeleEnemyAttack : MonoBehaviour
{
    [SerializeField] UnitSO unitSO;

    EnemyController enemyController;

    private GameObject target;
    
    //private Animator animator;

    private float attackTime;
    private float SmoothSpeed=0.5f;
    private float distanceToClosestTarget;
    private float distanceToTarget;
    GameObject[] allEnemy;

    private bool walk;

    bool attack;
    private void OnEnable()
    {
        enemyController = GetComponent<EnemyController>();
        //animator = GetComponent<Animator>();
        attackTime = 0;
        walk = true;
        //animator.SetTrigger("Walk");
    }
    private void Update()
    {
        if (attack)
        {
            walk = false;
            AttackTheCharacter();
            attackTime -= Time.deltaTime;
        }
        else
        {
            walk = true;

        }

        if (/*unitSO.startGame &&*/ walk)
        {   //animator.SetTrigger("Walk");

            findNearEnemy();
            transform.Translate(transform.forward * Time.deltaTime * -1 * unitSO.unitSpeed* SmoothSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            attack = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {

            attack = false;
            GameManager.Instance.GameOn = true;
        }
    }
    void AttackTheCharacter()
    {
        if (!target.activeInHierarchy)
        {
            attack = false;
            return;

        }
        switch (attackTime)
        {
            case <= 0:
                target.GetComponent<CharacterController>().TakeDamage(enemyController.enemyLevel * 10f);
                attackTime = unitSO.unitAttackSpeed;
                //animator.SetTrigger("Attack");
                GameManager.Instance.GameOn = true;
                break;
        }
    }

    void findNearEnemy()
    {
        distanceToClosestTarget = Mathf.Infinity;
        allEnemy = GameObject.FindGameObjectsWithTag("Character");
        if (allEnemy.Length == 0)
        {
            Debug.Log("GAME OVER");
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
}
