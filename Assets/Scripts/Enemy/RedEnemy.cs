using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new RedEnemyPatrolState();
        chaseState = new RedEnemyChaseState();
        foundPlayer = new EnemyFoundPlayerState();
        attack = new RedEnemyAttackState();
        hurt = new RedEnemyHurtState();
        dead = new EnemyDeadState();

    }
}
