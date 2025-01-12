using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Contants", menuName = "Contants", order = 1)]
public class Contants : ScriptableObject
{
   [Header("Collider")] 
   public Vector2 colliderSize;
   public float MaxDectedGround;
   public LayerMask groundLayer;

   [Header("Run")] public float RunReduce;
   public float RunAccel;
   public float AirMult;
   public float MaxRun;

   [Header("Jump")] 
   public float JumpGraceTimer;
   public float JumpBoost;
   public float JumpSpeed;
   public float VarJumpTimer;
   public float MaxFall;
   public float ShreldFallSpeed;
   public float Gravity;
   
   public int MaxJumps;
   public float SecondJumpSpeed;

   [Header("WallJump")]
   public float ClimbCheckDist;
   public float ClimbGrabYMult;
   
   public float WallJumpForceTime;
   public float WallJumpHSpeed;
   
   public float SuperJumpH;
   public float SuperWallJumpH;
   public float SuperWallJumpSpeed;

  

   [Header("Dash")]
   public float DashSpeed;
   public float DashTime;
   public float MaxDashes;
   public float EndDashSpeed;
   public float DashCooldown;
   public float DashRefillCooldown;

   [Header("Attack")] public float AttackCooldownTimer;
   public float AttackDecreaseX;
   public float AttackDecreaseY;
   public float AttackSpeed;
   
   [Header("Slide")]
   public float MaxSlideSpeed;
   
   [Header("Climb")]
   public float ClimbSpeed;
   public float ClimbCooldownTimer;
   public float ClimbDecreasesY;

   [Header("HopWait")] public float ClimbHopX;
   public float ClimbHopY;
   public float ClimbHopForceTime;
}
