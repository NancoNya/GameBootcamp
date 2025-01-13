using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class BlueEnemyPatrolState : EnemyBaseState
{
    private int randomNumber;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        currentEnemy.StartCoroutine(GenerateRandomNumber());

    }

    public override void Update()
    {
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.FoundPlayer);
        }

        
        
        if (!currentEnemy.physicsCheck.isGround||(currentEnemy.physicsCheck.touchLeftWall&&currentEnemy.faceDir.x<0) || (currentEnemy.physicsCheck.touchRightWall&&currentEnemy.faceDir.x>0))
        {
            currentEnemy.wait = true;
            currentEnemy.currentSpeed = 0;
            currentEnemy.anim.SetBool("walk", false);
        }
        else
        {
            if (randomNumber > 8)
            {
                currentEnemy.wait = true;
                currentEnemy.currentSpeed = 0;
                currentEnemy.anim.SetBool("walk",false);
            }
        }
    }

    public override void FixedUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.StopCoroutine(GenerateRandomNumber());
    }
    private IEnumerator GenerateRandomNumber()
    {
        Random random = new Random((uint)System.DateTime.Now.Ticks);
        while (true)
        {
             randomNumber = random.NextInt(0, 11); // 生成0到10之间的随机数
            yield return new WaitForSeconds(2f); // 每两秒执行一次
        }
    }
  
    
    
}
