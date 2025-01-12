using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Vector2 inputDirection;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;

    //������
    private Animator anim;
    public bool canDoubleJump = false;

    [Header("��������")]
    public float speed;
    public float jumpForce;
    public float doubleJumpForce;

    [Header("״̬")]
    public bool isAttack;
    public bool isHurt;
    public bool isDead;

    public float hurtForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        anim = GetComponent<Animator>();
        playerAnimation = GetComponent<PlayerAnimation>();

        inputControl = new PlayerInputControl();

        inputControl.Gameplay.Jump.started += Jump;
        inputControl.Gameplay.Attack.started += PlayerAttack;
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        //���﷭ת
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (physicsCheck.isGround)
            {
                anim.SetBool("jump", true);
                canDoubleJump = true;
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }

            else if (canDoubleJump) //������
            {
                anim.SetBool("doubleJump", true);
                canDoubleJump = false;
                rb.AddForce(transform.up * doubleJumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //��ɫ��أ����ö�������
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("jump", false);
            anim.SetBool("doubleJump", false);
        }
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;   //�����򵯿�
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);   //����ʩ�ӵ���

    }

    public void PlayerDead()   //��������
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
}
