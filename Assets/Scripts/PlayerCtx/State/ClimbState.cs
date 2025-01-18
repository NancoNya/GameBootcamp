using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : PlayerState
{
    private bool ClimbEnd;
    
    public ClimbState(PlayerController playerController) : base(EActionState.climb, playerController){}
    
    public override void Start()
    {
        playerController.SpeedX = 0f;
        playerController.SpeedY = playerController.contants.ClimbSpeed;
        playerController.player.playerRigidbody.gravityScale = 0f;

        ClimbEnd = false;
    }

  
    public override EActionState Update(float deltaTime)
    {
        //Debug.Log("Climb State");

        if (!playerController.ClimbCheck(playerController.Facing) || playerController.ClimbTopCheck())
        {
            return EActionState.normal;
        }
        
        /*if ((int)GameInput.Aim.value.x == playerController.Facing && !ClimbEnd && !playerController.ClimbTopCheck())
        {
            playerController.SpeedY = Mathf.Max(0,
                Mathf.MoveTowards(playerController.SpeedY, 0f, playerController.contants.ClimbDecreasesY * deltaTime));
        }
        else if ((int)GameInput.Aim.value.x != playerController.Facing)
        {
            ClimbEnd = true;
            playerController.SpeedY = 0f;
        }*/

        if (playerController.SlipCheck())
        {
            //Debug.Log("SlipCheck");
            ClimbHop();
            return EActionState.normal;
        }

        if (playerController.CanDash)
        {
            return playerController.Dash();
        }
        
        if (GameInput.Jump.Pressed())
        {
                if (playerController.ClimbCheck(1))
                {
                    if ((Facings)playerController.Facing == Facings.Right)
                    {
                        playerController.WallJump(-1);
                        return EActionState.normal;
                    }
                }
                
                if (playerController.ClimbCheck(-1))
                {
                    if ((Facings)playerController.Facing == Facings.Left)
                    {
                        playerController.WallJump(1);
                        return EActionState.normal;
                    }
                }
        }
        
        float mult = playerController.OnGround ? 1 : playerController.contants.AirMult;
        float max = playerController.contants.MaxRun;
        if ((int)GameInput.Aim.value.x == -playerController.Facing)
        {
            playerController.SpeedX = Mathf.MoveTowards(playerController.SpeedX, max * GameInput.Aim.value.x,
                mult * playerController.contants.RunAccel * Time.deltaTime);
            return EActionState.normal;
        }
        
        return stateStyle;
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
        playerController.ClimbCooldownTimer = playerController.contants.ClimbCooldownTimer;
        playerController.player.playerRigidbody.gravityScale = 1f;
    }
    
    public override IEnumerator Coroutine()
    {
        yield return null;
    }

    public override bool IsCoroutine()
    {
        return false;
    }

    private void ClimbHop()
    {
        bool hit = playerController.ColliderCheck(Vector2.zero, Vector2.right * playerController.Facing);
        if (hit)
        {
            playerController.HopWaitX = playerController.Facing;
            playerController.HopWaitXSpeed = playerController.Facing;
        }
        else
        {
            playerController.HopWaitX = 0;
            playerController.SpeedX = playerController.Facing * playerController.contants.ClimbHopX;
        }

        playerController.SpeedY = Mathf.Max(playerController.SpeedY, playerController.contants.ClimbHopY);

        playerController.ForceMoveX = playerController.Facing;
        playerController.ForceMoveTimer = playerController.contants.ClimbHopForceTime;
    }

}
