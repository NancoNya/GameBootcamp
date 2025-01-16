using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1State : EnemyBaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.SetTrigger("attack1");
    }

    public override void Update()
    {
       
    }

    public override void FixedUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
