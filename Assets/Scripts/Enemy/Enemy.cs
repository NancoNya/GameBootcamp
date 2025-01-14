using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public  Animator anim;
    public PhysicsCheck physicsCheck;

    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public Vector3 faceDirNoNormalized;
    

    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;

    [Header("追逐参数")]
    public float distanceToPlayer;
    public float detectionRadius;
    
    [Header("状态")]
    protected EnemyBaseState currentState;
    protected EnemyBaseState patrolState;
    protected EnemyBaseState chaseState;
    protected EnemyBaseState foundPlayer;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        faceDirNoNormalized = new Vector3(-transform.localScale.x, 0, 0);
        if(faceDirNoNormalized.x>0)faceDir=new Vector3(1, 0, 0);
        else faceDir=new Vector3(-1, 0, 0);
        
        
        currentState.Update();
        TimeCounter();
    }

    private void FixedUpdate()
    {
        Move();
        
        currentState.FixedUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x, rb.velocity.y);
    }

    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                currentSpeed=normalSpeed;
                waitTimeCounter = waitTime;
                //anim.SetBool("walk", true);
                transform.localScale = new Vector3(faceDirNoNormalized.x,transform.localScale.y ,transform.localScale.z ); ;  //等待后再转身
            }
        }
        
        if (!FoundPlayer() && lostTimeCounter > 0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
        else if (FoundPlayer())
        {
            lostTimeCounter = lostTime;
        }
    }
    
    public virtual bool FoundPlayer()
    {
        bool foundPlayer = false;
        foundPlayer=Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer)||
                    Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, -faceDir, checkDistance/3, attackLayer);
        
        return foundPlayer;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position + (Vector3)centerOffset, faceDir * checkDistance);
        Gizmos.DrawRay(transform.position + (Vector3)centerOffset, -faceDir * checkDistance/3);
    }
    
    /// <summary>
    /// 状态机转换
    /// </summary>
    /// <param name="state"></param>
    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.FoundPlayer=>foundPlayer,
            
            _ => null
        };
        
        
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    } 
    
    
   

}
