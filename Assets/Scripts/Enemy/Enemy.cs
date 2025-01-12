using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    protected Animator anim;
    PhysicsCheck physicsCheck;

    [Header("��������")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;

    [Header("���")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    [Header("��ʱ��")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;

    [Header("׷�����")]
    public float distanceToPlayer;
    public float detectionRadius;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        if (physicsCheck.touchLeftWall || physicsCheck.touchRightWall)
        {
            wait = true;
        }

        TimeCounter();
    }

    private void FixedUpdate()
    {
        Move();
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
                waitTimeCounter = waitTime;
                anim.SetBool("walk", true);
                transform.localScale = new Vector3(faceDir.x, 1, 1); ;  //�ȴ�����ת��
            }
        }
    }
}
