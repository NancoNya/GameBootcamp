
using UnityEngine;

public class JumpGrace
{
    private PlayerController _playerController;
    private float jumpGraceCount;
    public bool jumpGrace;

    public JumpGrace(PlayerController playerController, bool jumpGrace)
    {
        _playerController = playerController;
        ResetTime();
        this.jumpGrace = jumpGrace;
    }

    public void Update(float deltaTime)
    {
        if (_playerController.OnGround)
        {
            jumpGraceCount = _playerController.contants.JumpGraceTimer;
        }
        else
        {
            jumpGraceCount -= deltaTime;
        }
    }
    
    public void ResetTime() => jumpGraceCount = 0;

    public bool AllowJumpGrace() => jumpGrace ? jumpGraceCount > 0 : _playerController.OnGround;
}
