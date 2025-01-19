using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpProp : MonoBehaviour
{
    public bool isPlayerInTrigger = false;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            player = null;
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("press F");
            gameObject.SetActive(false);

            // 捡起HPProp1
            if (gameObject.CompareTag("HPProp1"))
            {
                Debug.Log("捡起的道具为HPProp1");
                if (player != null)
                {
                    Debug.Log("获取到player");
                    Character playerCharacter = player.GetComponent<Character>();
                    Debug.Log("获取到player的Character脚本");

                    if (playerCharacter != null)
                    {
                        float currentHealth = playerCharacter.currentHealth;
                        float maxHealth = playerCharacter.maxHealth;
                        // 增加玩家 10% 的生命值
                        playerCharacter.currentHealth = Mathf.Min(currentHealth + 0.1f * maxHealth, maxHealth);
                        Debug.Log("player's currentHealth increased by 10%");
                    }
                }
            }
        }
    }
}

