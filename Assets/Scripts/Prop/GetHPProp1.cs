using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHPProp1 : MonoBehaviour, IPropPickup
{
    public void Pickup(GameObject player)
    {
        Character playerCharacter = player.GetComponent<Character>();
        Debug.Log("��ȡ��player");

        if (playerCharacter != null)
        {
            float currentHealth = playerCharacter.currentHealth;
            float maxHealth = playerCharacter.maxHealth;
            //��������ֵ
            playerCharacter.currentHealth = Mathf.Min(currentHealth + 0.1f * maxHealth, maxHealth);
            Debug.Log("player's health increased by 10%");
        }
        else
        {
            Debug.LogError("The Player object does not have a Character component.");
        }
    }
}
