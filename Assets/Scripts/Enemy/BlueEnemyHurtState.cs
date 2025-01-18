using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyHurtState : EnemyBaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.transform.localScale = new Vector2(Player.Instance.playerController.Facing * 0.09f, 0.09f);
        //currentEnemy.currentSpeed = 0;
        currentEnemy.rb.velocity = new Vector2(10f * Player.Instance.playerController.Facing,  1);
        currentEnemy.anim.SetTrigger("hurt");
        Debug.Log("Hurt");
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
