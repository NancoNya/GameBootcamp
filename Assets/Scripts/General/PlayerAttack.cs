using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Attack Attack;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attack Enemy");
        Debug.Log("Damage  " + Attack.attackDamage);
        if (collision.CompareTag("Enemy"))
            collision.GetComponentInParent<Character>()?.GetDamage(Attack);
    }
}
