using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Attack Attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.CompareTag("Enemy"))
        {
            AttackScene.Instance.HitPause(0.1f);
            AttackScene.Instance.CameraShake(.1f);
            collision.GetComponentInParent<Character>()?.GetDamage(Attack);
        }

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        
    }
    
}
