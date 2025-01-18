using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Attack Attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AttackScene.Instance.HitPause(0.1f);
            AttackScene.Instance.CameraShake(.1f);
            collision.GetComponentInParent<Character>()?.GetDamage(Attack);
        }
    }
}
