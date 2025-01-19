using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedReady : MonoBehaviour
{
   public void PlayRedTail()
   {
       StartCoroutine(PlayRedTailCoroutine());
   }

   public IEnumerator PlayRedTailCoroutine()
   {
       transform.parent = null;
      Player.Instance.PlayRedTail();
      yield return new WaitForSeconds(1.8f);
      transform.parent = Player.Instance.transform;
      transform.position = new Vector3(Player.Instance.transform.position.x + 2.7f * Player.Instance.playerController.Facing, transform.position.y, transform.position.z);
   }
}
