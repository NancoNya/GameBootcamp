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
    
    public void DieAni() => _animator.SetTrigger(AniPara.Dead.ToString());

    public void PlayAttack1() => AudioManager.Instance.Playattack1();
    public void PlayAttack2() => AudioManager.Instance.Playattack2();
    public void PlayAttack3() => AudioManager.Instance.Playattack3();
    public void PlayRun() => AudioManager.Instance.Playrun();
    public void PlayJump() => AudioManager.Instance.Playjump();
    public void PlayRush() => AudioManager.Instance.Playrush();
    public void PlayHeartBreak() => AudioManager.Instance.PlayHeartBreak();
    public void PlayWalk() => AudioManager.Instance.Playwalk();

}
