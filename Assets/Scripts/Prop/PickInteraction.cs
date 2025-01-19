using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickInteraction : MonoBehaviour
{
    public bool isPlayerInTrigger = false;
    public GameObject player;

    //物体进入
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ontrigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("player close to prop");
            isPlayerInTrigger = true;
            player = other.gameObject;
        }
    }

    //物体离开
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            player = null;
        }
    }

    // 每帧调用一次
    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            IPropPickup iPropPickup = GetComponent<IPropPickup>();
            if (iPropPickup != null)
            {
                iPropPickup.Pickup(player);
                // 将道具设置为非激活状态
                gameObject.SetActive(false);
            }
        }
    }
}
