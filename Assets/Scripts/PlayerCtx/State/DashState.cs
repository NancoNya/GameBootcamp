using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DashState : PlayerState
{
    private int DashDir;
    private float beforeDashSpeed;
    
    public DashState(PlayerController ctx) : base(EActionState.dash, ctx)
    {
    }

    public override void Start()
    {
        playerController.DashCooldownTimer = playerController.contants.DashCooldown;
        playerController.DashRefillCooldownTimer = playerController.contants.DashRefillCooldown;
        
        playerController.EffectControl.Freeze(0.05f);
        
        beforeDashSpeed = playerController.SpeedX;
        
        playerController.SpeedX = 0f;
        playerController.SpeedY = 0f;
        DashDir = 0;
    }

   

    public override EActionState Update(float deltaTime)
    {
        
            if (GameInput.Jump.Pressed() && playerController.jumpGrace.AllowJumpGrace())
            {
                playerController.SuperJump();
                return EActionState.normal;
            }
        
            //向上Dash情况下，检测SuperWallJump
            if (GameInput.Jump.Pressed())
            {
                if (playerController.ClimbCheck(1))
                {
                    playerController.SuperWallJump(-1);
                    return EActionState.normal;
                }
                else if (playerController.ClimbCheck(-1))
                {
                    playerController.SuperWallJump(1);
                    return EActionState.normal;
                }
            }
            
            return stateStyle;
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Exit()
    {
       playerController.aniCtx.SB(AniPara.Dash.ToString(), false);
    }
    
    public override IEnumerator Coroutine()
    {
        yield return null;
        
        playerController.aniCtx.SB(AniPara.Dash.ToString(), true);
        
        var dir = playerController.Facing;
        var newSpeed = dir * playerController.contants.DashSpeed;

        if (Math.Sign(beforeDashSpeed) == Math.Sign(newSpeed) && Math.Abs(beforeDashSpeed) > Math.Abs(newSpeed))
        {
            newSpeed = beforeDashSpeed;
        }
        playerController.SpeedX = newSpeed;

        DashDir = dir;
        if (DashDir != 0)
        {
            playerController.Facing = Math.Sign(DashDir);
        }
        
        //特效
        
        yield return playerController.contants.DashTime;

        playerController.SpeedX = DashDir * playerController.contants.EndDashSpeed;

        playerController.stateMachine.State = (int)EActionState.normal;
    }

    public override bool IsCoroutine()
    {
        return true;
    }
}
