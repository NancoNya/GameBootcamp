using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunState : EnemyBaseState
{
    
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
     
        
    }

    public override void Update()
    {
        if (currentEnemy.FoundPlayer())
        {
            if (currentEnemy.attacker.position.x >= currentEnemy.transform.position.x)
            {
                currentEnemy.transform.localScale = new Vector3(Mathf.Abs(currentEnemy.faceDirNoNormalized.x),currentEnemy.transform.localScale.y ,currentEnemy.transform.localScale.z );
            }
            else
            {
                currentEnemy.transform.localScale = new Vector3(-Mathf.Abs(currentEnemy.faceDirNoNormalized.x),currentEnemy.transform.localScale.y ,currentEnemy.transform.localScale.z );
            }
            currentEnemy.distanceToPlayer=Vector2.Distance(currentEnemy.transform.position, currentEnemy.attacker.position);
            if (currentEnemy.distanceToPlayer <= currentEnemy.stopDistance)
            {
                currentEnemy.SwitchState(NPCState.BossAttack2);
            }
         
            
        }
        if (!currentEnemy.physicsCheck.isGround||(currentEnemy.physicsCheck.touchLeftWall&&currentEnemy.faceDir.x<0) || (currentEnemy.physicsCheck.touchRightWall&&currentEnemy.faceDir.x>0))
        {
            currentEnemy.transform.localScale = new Vector3(-currentEnemy.faceDirNoNormalized.x,currentEnemy.transform.localScale.y ,currentEnemy.transform.localScale.z );
        }
        
        
    }

    public override void FixedUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.currentSpeed = 0;
    }

}
