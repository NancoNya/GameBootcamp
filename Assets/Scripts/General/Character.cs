using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    public float attackPower;
    public float defensePower;

    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    public bool OneHit_Kill;
    public bool DoubleHurt;
    public bool ThreeHurt;

    public UnityEvent<Transform> OnTakeDamage;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (invulnerable)
            invulnerableCounter -= Time.deltaTime;
        if (invulnerableCounter <= 0)
            invulnerable = false;
    }

    public void GetDamage(float damage)
    {
        if (invulnerable || currentHealth <= 0)
            return;

        //if (currentHealth - attacker.attackPower > 0) //满足受伤条件
        {
            var Damage = damage;
            if (DoubleHurt) Damage *= 2;
            if (ThreeHurt) Damage *= 3;
            currentHealth = Mathf.Max(0, currentHealth - Damage);
            TriggerInvulnerable();

           // OnTakeDamage?.Invoke();
        }
        //else
        {
            if (currentHealth <= 0)
            {
                //死亡
            }
        }
    }

    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }

}
