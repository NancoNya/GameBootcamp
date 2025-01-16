using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack 
{
    public Transform transform;
    public Attack(Transform transform, int attackPower, float attackDamage)
    {
        this.transform = transform;
        this.attackDamage = attackDamage;
        this.attackPower = attackPower;
    }
    
    public int attackPower;
    public float attackDamage;
}
