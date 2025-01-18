using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurtState : EnemyBaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.transform.localScale = new Vector2(Player.Instance.playerController.Facing, 1);
        //currentEnemy.currentSpeed = 0;
        currentEnemy.rb.velocity = new Vector2(20f * Player.Instance.playerController.Facing,  1);
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
