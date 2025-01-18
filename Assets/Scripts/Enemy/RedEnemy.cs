using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{
    public Animator hitAnimator;
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
    
    public void GetHit()
    {
        faceDir = new Vector2(Player.Instance.playerController.Facing, 1);
        GetComponent<Rigidbody2D>().AddForce(3f * -faceDir, ForceMode2D.Impulse);
        hitAnimator.SetTrigger("Hit");
    }
}
