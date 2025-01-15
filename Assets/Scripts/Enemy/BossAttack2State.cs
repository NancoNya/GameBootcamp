using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2State : EnemyBaseState
{
    

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.SetTrigger("attack2");
        
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
