using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
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

    [Header("受伤动画cd")]
    public float hurtTime;
    [HideInInspector]public float hurtCounter;
    public bool isHurt;

    public bool OneHit_Kill;
    public bool DoubleHurt;
    public bool ThreeHurt;
    

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;

    public UnityEvent OnDie;

    
    private void NewGame()
    {
        if(!transform.CompareTag("Player"))currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }
    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (invulnerable)
            invulnerableCounter -= Time.deltaTime;
        if (invulnerableCounter <= 0)
            invulnerable = false;

        if (isHurt)
            hurtCounter -= Time.deltaTime;
        if (hurtCounter <= 0)
            isHurt = false;
    }

    public void GetDamage(Attack attacker)
    {
        if (invulnerable || currentHealth <= 0)
            return;

        var Damage = attacker.attackDamage;
       
            if (DoubleHurt) Damage *= 2;
            if (ThreeHurt) Damage *= 3;
            if (currentHealth - Damage > 0)
            {
                currentHealth -= Damage;
                //受伤伤害后触发无敌计时
                TriggerInvulnerable();
            
                //执行受伤事件
                OnTakeDamage?.Invoke(attacker.transform);
            }
            else
            {
                currentHealth = 0;
                //触发死亡
                OnDie?.Invoke();
            }
        
            OnHealthChange?.Invoke(this);
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
