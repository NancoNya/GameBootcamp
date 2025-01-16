using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BossHealthChange : MonoBehaviour
{
    public Image healthImage;
    private void Update()
    {
        
    }

    public void Change(Character character)
    {
        healthImage.fillAmount = character.currentHealth/character.maxHealth;
    }
}
