using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Attack Attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>()?.GetDamage(Attack);
            Player.Instance.Hurt(GetComponentInParent<Enemy>().faceDir.x);
        }
    }
}

