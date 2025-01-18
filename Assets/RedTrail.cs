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
        yield return new WaitForSeconds(2.5f);
        transform.parent = Player.Instance.transform;
    }
    
}
