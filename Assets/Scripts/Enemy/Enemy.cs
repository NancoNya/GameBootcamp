using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public PhysicsCheck physicsCheck;
    public Vector3 enemyPosition;
    public Vector3 dropPropPosition;

    [Header("移动参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    [HideInInspector] public Vector3 faceDir;
    [HideInInspector] public Vector3 faceDirNoNormalized;

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
    public Transform attacker;
    public float stopDistance = 1.5f;
    public float distanceToPlayer;
    public float detectionRadius;

    [Header("状态")]
    public bool isDead;
    protected EnemyBaseState currentState;
    protected EnemyBaseState patrolState;
    protected EnemyBaseState chaseState;
    protected EnemyBaseState foundPlayer;
    protected EnemyBaseState bossAttack1;
    protected EnemyBaseState bossAttack2;
    protected EnemyBaseState hurt;
    protected EnemyBaseState dead;
    protected EnemyBaseState attack;

    [Header("属性")]
    public float maxHealth;
    public float currentHealth;
    public float attackPower;
    public float defensePower;

    public AudioSource audioSource;

    [Header("死亡掉落道具")]
    public GameObject buffProp;
    public GameObject HPProp1;
    public GameObject HPProp2;
    public GameObject HPProp3;
    public GameObject killProp;

    //返回随机掉落道具的函数
    public GameObject SpawnRandomPrefab()
    {
        GameObject[] prefabs = new GameObject[] { buffProp, HPProp1, HPProp2, HPProp3, killProp };
        int randomIndex = Random.Range(0, prefabs.Length);
        return Instantiate(prefabs[randomIndex]);
    }

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

    protected virtual void Update()
    {
        faceDirNoNormalized = new Vector3(-transform.localScale.x, 0, 0);
        if (faceDirNoNormalized.x > 0) faceDir = new Vector3(1, 0, 0);
        else faceDir = new Vector3(-1, 0, 0);

        currentState.Update();
        TimeCounter();
    }

    private void FixedUpdate()
    {
        Move();

        //道具掉落位置
        enemyPosition = GetComponent<Transform>().position;
        dropPropPosition = enemyPosition;
        dropPropPosition.y += 1f;

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
                currentSpeed = normalSpeed;
                waitTimeCounter = waitTime;
                //anim.SetBool("walk", true);
                transform.localScale = new Vector3(faceDirNoNormalized.x, transform.localScale.y, transform.localScale.z); ;  //等待后再转身
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
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + (Vector3)centerOffset, faceDir, checkDistance, attackLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + (Vector3)centerOffset, -faceDir, checkDistance / 3, attackLayer);
        bool foundPlayer = hit1.collider != null || hit2.collider != null;
        if (foundPlayer)
        {
            if (hit1.collider != null)
            {
                attacker = hit1.transform;
            }
            else if (hit2.collider != null)
            {
                attacker = hit2.transform;
            }
        }
        return foundPlayer;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position + (Vector3)centerOffset, faceDir * checkDistance);
        Gizmos.DrawRay(transform.position + (Vector3)centerOffset, -faceDir * checkDistance / 3);
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
            NPCState.FoundPlayer => foundPlayer,
            NPCState.BossAttack1 => bossAttack1,
            NPCState.BossAttack2 => bossAttack2,
            NPCState.Dead => dead,
            NPCState.Hurt => hurt,
            NPCState.Attack => attack,
            _ => null
        };


        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    public void EnemyHurt()
    {
        SwitchState(NPCState.Hurt);
    }

    public void EnemyDead()
    {
        // 存储预制体的数组
        GameObject[] prefabs = new GameObject[] { buffProp, HPProp1, HPProp2, HPProp3, killProp };

        // 生成一个 0 到 4 的随机索引
        int randomIndex = Random.Range(0, prefabs.Length);

        // 实例化随机选择的预制体
        GameObject newProp = Instantiate(prefabs[randomIndex], dropPropPosition, Quaternion.identity);
        Debug.Log("随机掉落道具");

        SwitchState(NPCState.Dead);
    }

    public void AfterDeadAnimation()
    {
        Destroy(this.gameObject);
    }

    public void PlayEnemyAttack1() => AudioManager.Instance.PlayenemyAttack1(audioSource);

    public void PlayEnemyAttack2() => AudioManager.Instance.PlayenemyAttack2(audioSource);
    public void PlayEnemyAttack3() => AudioManager.Instance.PlayenemyAttack3(audioSource);
    public void PlayEnemyHurt1() => AudioManager.Instance.PlayenemyHurt1(audioSource);
    public void PlayEnemyHurt2() => AudioManager.Instance.PlayenemyHurt2(audioSource);
    public void PlayEnemyHurt3() => AudioManager.Instance.PlayenemyHurt3(audioSource);
    public void PlayEnemyRun() => AudioManager.Instance.PlayenemyRun(audioSource);
}

