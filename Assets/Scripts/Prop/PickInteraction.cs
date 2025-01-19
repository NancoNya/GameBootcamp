using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickInteraction : MonoBehaviour
{
    public bool isPlayerInTrigger = false;
    public GameObject player;

    //�������
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

    //�����뿪
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            player = null;
        }
    }

    // ÿ֡����һ��
    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            IPropPickup iPropPickup = GetComponent<IPropPickup>();
            if (iPropPickup != null)
            {
                iPropPickup.Pickup(player);
                // ����������Ϊ�Ǽ���״̬
                gameObject.SetActive(false);
            }
        }
    }
}
