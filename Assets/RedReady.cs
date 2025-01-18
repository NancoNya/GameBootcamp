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
      yield return new WaitForSeconds(2f);
      transform.parent = Player.Instance.transform;
   }
}
