using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTrail : MonoBehaviour
{
    public void SetRedTrail()
    {
        StartCoroutine(SetForRedTrail());
    }

    private IEnumerator SetForRedTrail()
    {
        transform.parent = null;
        yield return new WaitForSeconds(2.3f);
        transform.parent = Player.Instance.transform;
        transform.position = new Vector3(Player.Instance.transform.position.x + 2.7f * Player.Instance.playerController.Facing, transform.position.y, transform.position.z);
    }
    
}
