using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    public Animator hitAnimator;
    public bool isHit;
    
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

    protected override void Update()
    {
            base.Update();
    }

    public void GetHit()
    {
        Hit();
    }

    public void Hit()
    {
        isHit = true;
        
        GetComponent<Rigidbody2D>().AddForce(10f * faceDir, ForceMode2D.Impulse);
        hitAnimator.SetTrigger("Hit");
        isHit = false;
    }
}
