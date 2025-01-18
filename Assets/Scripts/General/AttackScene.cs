using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AttackScene : MonoSigleton<AttackScene>
{
    public bool isShake;
    public CinemachineCollisionImpulseSource impulseSource;

    private void Start()
    {
        impulseSource = GetComponent<CinemachineCollisionImpulseSource>();
    }

    public void HitPause(float duration)
    {
        Player.Instance.Freeze(duration);
    }
    

    public void CameraShake(float strength)
    {
        impulseSource.GenerateImpulseWithForce(strength);
    }

    public void GetHit()
    {
        
    }
}
