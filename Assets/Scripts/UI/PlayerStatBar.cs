using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Character currentCharacter;
    public Image healthImage;
    public Image healthDelayImage;

    public Image SkillImage;
    public Image FiniteSkillImage;
    
    private bool isRecovering;
    private void Update()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }

        OnSkillChange();
        OnFiniteSkillChange();
    }

    /// <summary>
    /// 接收Health的变更百分比
    /// </summary>
    /// <param name="persentage">百分比：Current/Max</param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }

    public void OnSkillChange()
    {
        SkillImage.fillAmount = currentCharacter.currentSkill / 100f;
    }

    public void OnFiniteSkillChange()
    {
        FiniteSkillImage.fillAmount = currentCharacter.currentFiniteSkill / 100f;
    }
    

}

