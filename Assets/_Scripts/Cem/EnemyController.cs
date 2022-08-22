using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float _health;

    public int enemyLevel;
    Animator animator;
    Animation anim;
    private ReportLevelEnemyCount _reportLevelEnemyCount;

    [SerializeField] UnitSO unitSO;

    Slider healthBar;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        _health = unitSO.unitHealth * enemyLevel;
        healthBar = transform.GetChild(0).transform.GetChild(0).transform.gameObject.GetComponent<Slider>();
        healthBar.maxValue = _health;
        healthBar.value = _health;
        _reportLevelEnemyCount = GetComponent<ReportLevelEnemyCount>();

    }

    private void Update()
    {
        if (_health <= 0)
        {
            _reportLevelEnemyCount.hpUnder0 = true;
            animator.SetBool("Die", true);
            animator.SetBool("Attack", false);
            animator.SetBool("Run", false);
            this.GetComponent<EnemyController>().enabled = false;

        }
        healthBar.transform.LookAt(Vector3.forward);
        healthBar.transform.rotation = (Quaternion.LookRotation(Vector3.up));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);

        }
        if (other.gameObject.CompareTag("Character"))
        {
            TakeDamage(10);

        }

    }
    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;
        healthBar.value = _health;
    }

}
