using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    public Animator hitAnimator;
    
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
    
     public void GetHit()
    {
        faceDir = new Vector2(Player.Instance.playerController.Facing, 1);
        GetComponent<Rigidbody2D>().AddForce(3f * -faceDir, ForceMode2D.Impulse);
        hitAnimator.SetTrigger("Hit");
    }
}
