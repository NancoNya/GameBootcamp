using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AniPara
{
    VelocityY,
    VelocityX,
    isGround,
    Dash,
    Jump,
    Attack,
    ComboCount,
    slide,
    Dead,
    ReBound,
    Hurt
}

public class AniCtx 
{
    private PlayerController player;
    private Animator ani;

    public AniCtx(PlayerController player)
    {
        this.player = player;
        ani = player.player.animator;
    }

    public void Update(float deltaTime)
    {
        ani.SetFloat(AniPara.VelocityX.ToString(), player.SpeedX);
        ani.SetFloat(AniPara.VelocityY.ToString(), player.SpeedY);
        ani.SetBool(AniPara.isGround.ToString(), player.OnGround);
    }
    
    public void SB(string name, bool ch) => ani.SetBool(name, ch);
    
    public void ST(string name) => ani.SetTrigger(name);
    
    public void SI(string name, int value) => ani.SetInteger(name, value);

    public void RT(string name) => ani.ResetTrigger(name);
}
