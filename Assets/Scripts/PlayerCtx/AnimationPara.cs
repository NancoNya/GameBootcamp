using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPara : MonoBehaviour
{
    public Player _player;
    
    public void SetAttackEnd()
    {
        _player.playerController.AttackEnd = true;
    }
}
