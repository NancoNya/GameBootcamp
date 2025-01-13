using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoSigleton<Player>
{
    public PlayerController playerController;
    public BoxCollider2D boxCollider;
    public Rigidbody2D playerRigidbody;
    public Animator animator;
    
    public Contants contants;
    

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        playerController = new PlayerController(this);
        playerController.Init();
    }

    private void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;
        playerController.Update(deltaTime);
        //Debug.Log(CheckGround());
    }

    private void FixedUpdate()
    {
        playerController.FixedUpdate();
    }

    /*private bool CheckGround(Vector2 Dir)
    {
        return Physics2D.OverlapBox((Vector2)gameObject.transform.position , new Vector2(0, 0.5f), 0.5f, LayerMask.GetMask("Ground"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector2(0, 0.5f));
    }*/
}
