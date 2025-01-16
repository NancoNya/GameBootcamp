using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShowHealth : MonoBehaviour
{
    public float checkDistance;
    public LayerMask attackLayer;
    public GameObject bossHealth;
    private Boss boss;

    private void Awake()
    {
        boss=GetComponent<Boss>();
    }

    private void Update()
    {
        if (!boss.isDead)
        {
            var obj=Physics2D.OverlapCircle(transform.position, checkDistance, attackLayer);
            if (obj)
            {
                bossHealth.SetActive(true);
            }
            else
            {
                bossHealth.SetActive(false);
            }
        }
        else
        {
            bossHealth.SetActive(false);
        }
    }
    
    public  void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(transform.position, checkDistance);
        
    }
}
