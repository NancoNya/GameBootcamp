using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoSigleton<Player>, IEffectControl
{
    public PlayerController playerController;
    public BoxCollider2D boxCollider;
    public Rigidbody2D playerRigidbody;
    public Animator animator;
    
    public Contants contants;
    
    public float FreezeCooldownTimer;
    

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        playerController = new PlayerController(this);
    }

    private void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;
        
        if (UpdateTime(deltaTime))
        {
            GameInput.Update(deltaTime);
            playerController.Update(deltaTime);
        }
    }

    private void FixedUpdate()
    {
        playerController.FixedUpdate();
    }

    public bool UpdateTime(float deltaTime)
    {
        if (FreezeCooldownTimer > 0)
        {
            FreezeCooldownTimer = Mathf.Max(0, FreezeCooldownTimer - deltaTime);
            return false;
        }

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;   
        }
        
        return true;
    }

    public void Freeze(float freezeTime)
    {
        FreezeCooldownTimer = Mathf.Max(this.FreezeCooldownTimer, freezeTime);
        if (FreezeCooldownTimer > 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ChameraShake(float chargeTime)
    {
        
    }
}
