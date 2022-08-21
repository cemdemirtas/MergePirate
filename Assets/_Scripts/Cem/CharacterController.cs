using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public bool canHit;
    Animator animator;
    [SerializeField] int _characterLevel;
    [SerializeField] UnitSO unitSO;

    Slider healthBar;
    
    public float _health;
    
    public int characterLevel
    {
        get { return _characterLevel; }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        healthBar = transform.GetChild(0).transform.GetChild(0).transform.gameObject.GetComponent<Slider>();
        _health = unitSO.unitHealth * _characterLevel;
        healthBar.maxValue = _health;
        healthBar.value = _health;
        canHit = true;
    }

    private void Update()
    {   
        if (Input.touchCount == 0)
        {
            canHit = false;
        }
        healthBar.transform.LookAt(Vector3.forward);
        healthBar.transform.rotation = (Quaternion.LookRotation(Vector3.up));
    }

    public void TakeDamage(float _damageValue)
    {
        _health -= characterLevel * 10;
        healthBar.value = _health;
        if (_health <= 0)
        {
            animator.SetBool("Die", true);
            animator.SetBool("Attack", false);
            animator.SetBool("Run", false);
            this.GetComponent<CharacterController>().enabled = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(10);

        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);

        }
    }



}
