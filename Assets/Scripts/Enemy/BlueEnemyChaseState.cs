using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyChaseState : EnemyBaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        
        currentEnemy.anim.SetBool("walk",true);
        
    }

    public override void Update()
    {
        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        
        if (!currentEnemy.physicsCheck.isGround || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDirNoNormalized.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }
    }

    public override void FixedUpdate()
    {
       
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk",false);
    }
}
