using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurtState : EnemyBaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0;
        currentEnemy.anim.SetTrigger("hurt");
    }

    public override void Update()
    {
       
    }

    public override void FixedUpdate()
    {
       
    }

    public override void OnExit()
    {
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
}
