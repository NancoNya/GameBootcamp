using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BlueEnemyPatrolState();
        chaseState = new BlueEnemyChaseState();
        foundPlayer = new EnemyFoundPlayerState();
        attack = new BlueEnemyAttackState();
        hurt = new BlueEnemyHurtState();
        dead = new EnemyDeadState();
    }
    
}
