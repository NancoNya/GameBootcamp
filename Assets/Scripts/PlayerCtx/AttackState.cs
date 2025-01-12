using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    public AttackState(PlayerController ctx) : base(EActionState.attack, ctx)
    {
    }

    public override void Start()
    {
        
        playerController.SpeedY = 0;
        playerController.SpeedX = 0;
       
    }

    public override EActionState Update(float deltaTime)
    {
        
        if (playerController.CanDash)
        {
            return playerController.Dash();
        }
        
        if (GameInput.Jump.Pressed())
        {
            if (playerController.jumpGrace.AllowJumpGrace())
            {
                playerController.Jump();
                return EActionState.normal;
            }
        }
        
        if (playerController.AttackEnd)
        {
            return EActionState.normal;
        }

        return stateStyle;
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Exit()
    {
        if (playerController.AttackComobo == 3)
        {
            playerController.AttackComobo = 1;
        }
        else
        {
            playerController.AttackComobo++;
        }

        playerController.AttackCooldownTimer = playerController.contants.AttackCooldownTimer;
        playerController.AttackEnd = false;
        
        playerController.aniCtx.ST("attackExit");
    }

    public override IEnumerator Coroutine()
    {
        yield return null;

        playerController.SpeedX = playerController.contants.AttackSpeed * playerController.Facing;
        
        playerController.aniCtx.SI(AniPara.ComboCount.ToString(), playerController.AttackComobo);
        playerController.aniCtx.ST(AniPara.Attack.ToString());
    }

    public override bool IsCoroutine()
    {
        return true;
    }
}
