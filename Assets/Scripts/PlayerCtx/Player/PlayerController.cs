using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
   public Player player;
   public AniCtx aniCtx;
   public IEffectControl EffectControl;
   
   public Contants contants;
   public JumpGrace jumpGrace;
   
   public bool OnGround;
  
   public float VarJumpSpeed;
   public float VarJumpTimer;
   public float MaxFall;
   public bool CanSecondJump;

   public float ForceMoveTimer;
   public int ForceMoveX;

   public float DashCooldownTimer;
   public float DashRefillCooldownTimer;
   public float Dashes;

   public float AttackCooldownTimer;
   public int AttackComobo;
   public bool CanAttack => AttackCooldownTimer <= 0 && GameInput.Attack.Pressed();
   public bool AttackEnd;
   
   public bool CanDash => GameInput.Dash.Pressed() && DashCooldownTimer <= 0f && Dashes > 0;

   public float ClimbCooldownTimer;

   public float HopWaitX;
   public float HopWaitXSpeed;

   public float SpeedX
   {
      get => player.playerRigidbody.velocity.x;
      set => player.playerRigidbody.velocity = new Vector2(value, player.playerRigidbody.velocity.y);
   }

   public float SpeedY
   {
      get => player.playerRigidbody.velocity.y;
      set => player.playerRigidbody.velocity = new Vector2(player.playerRigidbody.velocity.x, value);
   }

   private int _facing;

   public int Facing
   {
      get => _facing;
      set
      {
         if (value == 0)
         {
            _facing = (int)Facings.Right;
         }
         else
         {
            _facing = value;
         }
         
         player.transform.localScale = new Vector3(_facing, 1, 1);
      }
   }

   public PlayerController(Player player)
   {
      this.player = player;
      this.contants = player.contants;
      Facing = (int)Facings.Right;
      
      stateMachine = new FiniteStateMachine<PlayerState>();
      aniCtx = new AniCtx(this);
      EffectControl = player.GetComponent<IEffectControl>();
      //if (EffectControl != null) Debug.Log("Effect control found");
      
      stateMachine.AddState(new NormalState(this));
      stateMachine.AddState(new ClimbState(this));
      stateMachine.AddState(new DashState(this));
      stateMachine.AddState(new AttackState(this));
      
      stateMachine.State = (int)EActionState.normal;
      jumpGrace = new JumpGrace(this, true);

      AttackComobo = 1;
      CanSecondJump = false;
   }
   
   public FiniteStateMachine<PlayerState> stateMachine;
   
   public EActionState Dash()
   {
      Dashes = Math.Max(0, Dashes - 1);
      GameInput.Dash.ConsumeBuffer();
      return EActionState.dash;
   }

   public void Update(float deltaTime)
   {
      stateMachine.Update(deltaTime);
      jumpGrace.Update(deltaTime);
      aniCtx.Update(deltaTime);

      OnGround = CheckGrounded();

      if (ForceMoveTimer > 0)
      {
         ForceMoveTimer -= deltaTime;
         Facing = ForceMoveX;
      }
      else if (GameInput.Aim.value.x != 0 && (int)GameInput.Aim.value.x != (int)Facing)
      {
         Facing = (int)GameInput.Aim.value.x;
      }

      if (VarJumpTimer > 0)
      {
         VarJumpTimer -= deltaTime;
      }

      if (DashCooldownTimer > 0)
      {
         DashCooldownTimer -= deltaTime;
      }

      if (DashRefillCooldownTimer > 0)
      {
         DashRefillCooldownTimer -= deltaTime;
      }
      else if (OnGround)
      {
         RefillDash();
      }

      if (AttackCooldownTimer > 0)
      {
         AttackCooldownTimer -= deltaTime;
      }

      if (ClimbCooldownTimer > 0)
      {
         ClimbCooldownTimer -= deltaTime;
      }
      
   }

   public void FixedUpdate()
   {
      stateMachine.FixedUpdate();
   }

   public void Jump()
   {
      GameInput.Jump.ConsumeBuffer();
      jumpGrace.ResetTime();
         
      SpeedX += contants.JumpBoost * GameInput.Aim.value.x;
      VarJumpTimer = contants.VarJumpTimer;
      
      aniCtx.ST(AniPara.Jump.ToString());
      
      SpeedY = contants.JumpSpeed;
      VarJumpSpeed = SpeedY;
   }
   
   public void SecondJump()
   {
      GameInput.Jump.ConsumeBuffer();
      jumpGrace.ResetTime();
         
      SpeedX += contants.JumpBoost * GameInput.Aim.value.x;
      VarJumpTimer = contants.VarJumpTimer;
      
      aniCtx.ST(AniPara.Jump.ToString());
      //Debug.Log("Jump");
      
      SpeedY = contants.SecondJumpSpeed;
      VarJumpSpeed = SpeedY;
   }

   public void WallJump(int dir)
   {
      GameInput.Jump.ConsumeBuffer();
      VarJumpTimer = contants.VarJumpTimer;

      if (GameInput.Aim.value.x != 0)
      {
         ForceMoveX = dir;
         ForceMoveTimer = contants.WallJumpForceTime;
      }
      
      aniCtx.ST(AniPara.Jump.ToString());
      
      SpeedX = contants.WallJumpHSpeed * dir;
      SpeedY = contants.JumpSpeed;

      VarJumpSpeed = SpeedY;
   }

   public void SuperJump()
   {
      GameInput.Jump.ConsumeBuffer();
      jumpGrace?.ResetTime();
      VarJumpTimer = contants.VarJumpTimer;

      aniCtx.ST(AniPara.Jump.ToString());
      
      SpeedX = contants.SuperJumpH * Facing;
      SpeedY = contants.JumpSpeed;
   }

   public void SuperWallJump(int dir)
   {
      GameInput.Jump.ConsumeBuffer();
      VarJumpTimer = contants.VarJumpTimer;
      jumpGrace?.ResetTime();
      
      aniCtx.ST(AniPara.Jump.ToString());
      
      SpeedX = contants.SuperWallJumpH * dir;
      SpeedY = contants.SuperWallJumpSpeed;
      
      VarJumpSpeed = SpeedY;
   }

   private bool RefillDash()
   {
      if (Dashes < contants.MaxDashes)
      {
         this.Dashes = contants.MaxDashes;
         return true;
      }

      return false; 
   }
}
