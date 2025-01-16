using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPara : MonoBehaviour
{
    public Player _player;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SetAttackEnd()
    {
        _player.playerController.AttackEnd = true;
    }
    
    public void Die() => Destroy(transform.parent.gameObject);
    
    public void DieAni() => _animator.SetBool(AniPara.Dead.ToString(), true);
    
}
