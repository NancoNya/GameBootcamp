using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;
    public float currentHealth;
    public float attackPower;
    public float defensePower;
    public float moveSpeed;

    [Header("�����޵�")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

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

    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
            return;

        if (currentHealth - attacker.attackPower > 0) //������������
        {
            currentHealth -= attacker.attackPower;
            TriggerInvulnerable();

            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            //����
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
