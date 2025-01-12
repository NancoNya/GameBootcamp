using System.Collections;
using UnityEngine;

public class NormalState : PlayerState
{
    public NormalState(PlayerController playerController) : base(EActionState.normal, playerController){}
    

    public override void Start()
    {
        //Debug.Log("idle start");
        playerController.MaxFall = playerController.contants.MaxFall;
    }

    public override EActionState Update(float deltaTime)
    {
        if (playerController.ClimbCheck(playerController.Facing) && (int)GameInput.Aim.value.x == playerController.Facing && playerController.ClimbCooldownTimer <= 0)
        {
            return EActionState.climb;
        }

        if (playerController.CanDash)
        {
            return playerController.Dash();
        }
        
        

        if (GameInput.Slide.Pressed() && playerController.OnGround)
        {
            playerController.aniCtx.ST(AniPara.slide.ToString());
            var slideSpeed = playerController.SpeedX > playerController.contants.MaxSlideSpeed ? playerController.SpeedX : playerController.contants.MaxSlideSpeed;
            playerController.SpeedX = slideSpeed * playerController.Facing;
        }

        if (playerController.CanAttack)
        {
            return EActionState.attack;
        }
        
        if (GameInput.Jump.Pressed() && playerController.CanSecondJump)
        {
            playerController.OnGround = true;
            playerController.SecondJump();
            playerController.CanSecondJump = false;
            Debug.Log("SecondJump");
        }
        
        if (!playerController.OnGround && !GameInput.Jump.Pressed())
        {
            float max = playerController.MaxFall;

            float mult = Mathf.Abs(playerController.SpeedY) < playerController.contants.ShreldFallSpeed && GameInput.Jump.Checked()
                ? .5f
                : 1f;

            playerController.SpeedY = Mathf.MoveTowards(playerController.SpeedY, max,
                mult * deltaTime * playerController.contants.Gravity);
        }

        if (GameInput.Jump.Checked())
        {
            if (playerController.VarJumpTimer > 0)
            {
                playerController.SpeedY = Mathf.Max(playerController.SpeedY, playerController.VarJumpSpeed);
            }
            else
            {
                playerController.VarJumpTimer = 0f;
            }
        }
       
        
        if (GameInput.Jump.Pressed())
        {
            if (playerController.jumpGrace.AllowJumpGrace())
            {
                playerController.Jump();
                playerController.CanSecondJump = true;
            }
            
            else
            {
                if (playerController.ClimbCheck(1))
                {
                    if ((Facings)playerController.Facing == Facings.Right)
                    {
                        playerController.WallJump(-1);
                    }
                }
                
                if (playerController.ClimbCheck(-1))
                {
                    if ((Facings)playerController.Facing == Facings.Left)
                    {
                        playerController.WallJump(1);
                    }
                }
            }

            
        }
        
        return stateStyle;
    }

    public override void FixedUpdate()
    {
        HorizontalMove();
    }
    
    public override void Exit()
    {
       
    }
    
    private void HorizontalMove()
    {
        float mult = playerController.OnGround ? 1 : playerController.contants.AirMult;
        float max = playerController.contants.MaxRun;
        if (Mathf.Abs(playerController.SpeedX) > max &&
            (int)Mathf.Sign(playerController.SpeedX) == (int)GameInput.Aim.value.x)
            playerController.SpeedX = Mathf.MoveTowards(playerController.SpeedX, max * GameInput.Aim.value.x,
                playerController.contants.RunReduce * mult * Time.deltaTime);
        else
        {
            playerController.SpeedX = Mathf.MoveTowards(playerController.SpeedX, max * GameInput.Aim.value.x, mult * playerController.contants.RunAccel * Time.deltaTime);
        }
    }
    
    public override IEnumerator Coroutine()
    {
        yield return null;
    }

    public override bool IsCoroutine()
    {
        return false;
    }
}
