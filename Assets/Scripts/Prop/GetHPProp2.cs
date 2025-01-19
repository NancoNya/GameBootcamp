using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHPProp2 : MonoBehaviour, IPropPickup
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
            playerCharacter.currentHealth = Mathf.Min(currentHealth + 0.25f * maxHealth, maxHealth);
            Debug.Log("player's health increased by 25%");
        }
        else
        {
            Debug.LogError("The Player object does not have a Character component.");
        }
    }
}
