using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFoundPlayerState : EnemyBaseState

{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0;
        currentEnemy.anim.SetTrigger("FoundPlayer");
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
