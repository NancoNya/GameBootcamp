using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    
    
    public bool CheckGrounded() => CheckGrounded(Vector2.zero);
    
    public bool CheckGrounded(Vector2 offset)
    {
        RaycastHit2D hit =
            Physics2D.BoxCast((Vector2)player.transform.position + offset, player.boxCollider.size, 0f, Vector2.down, contants.MaxDectedGround, contants.groundLayer);
        if (hit && hit.normal == Vector2.up)
        {
            return true;
        }
        return false;
    }

    public bool ColliderCheck(Vector2 offset, Vector2 Dir)
    {
        return Physics2D.OverlapBox((Vector2)player.transform.position + offset * Dir, player.boxCollider.size, 0f, contants.groundLayer);
    }

    public bool ClimbCheck(int dir)
    {
        return Physics2D.OverlapBox((Vector2)player.transform.position + Vector2.right * dir * (contants.ClimbCheckDist * 0.1f + 0.02f) + Vector2.up * 0.2f, player.boxCollider.size,
             0f, contants.groundLayer);
    }
    
    public bool SlipCheck(float addY = 0)
    {
        int direct = Facing;
        Vector2 origin = (Vector2)player.transform.position + Vector2.up * player.boxCollider.size.y / 2f + Vector2.right * direct * (player.boxCollider.size.x / 2f );
        Vector2 point1 = origin + Vector2.up * (-0.4f + addY);

        if(Physics2D.OverlapPoint(point1, contants.groundLayer))
        {
            return false;
        }
        Vector2 point2 = origin + Vector2.up * (0.4f + addY);
        if (Physics2D.OverlapPoint(point2, contants.groundLayer))
        {
            return false;
        }
        return true;
    }
}
