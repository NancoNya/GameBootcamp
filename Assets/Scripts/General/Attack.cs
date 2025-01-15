using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackPower;
    public float attackDamage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Character>()?.GetDamage(this.attackDamage);
    }
}
