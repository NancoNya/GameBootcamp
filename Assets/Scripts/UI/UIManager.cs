using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    public GameObject PausePanel;

    [Header("事件监听")] 
    public CharacterEventSO healthEvent;


   
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausePanel.activeSelf)
            {
                PausePanel.SetActive(false);
                SetTime(1, false);
            }
            else if (!PausePanel.activeSelf)
            {
                PausePanel.SetActive(true);
                SetTime(0, true);
            }
        }
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;

    }

    public void SetTime(int time, bool state)
    {
        Time.timeScale = time;
        Player.Instance.stop = state;
    }

    public void SetTime()
    {
        Time.timeScale = 0;
        Player.Instance.stop = true;
    }

    public void SetTime2()
    {
        Time.timeScale = 1;
        Player.Instance.stop = false;
    }

  

    private void OnHealthEvent(Character character)
    {
        var persentage=character.currentHealth/character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
      
   
    }
   
   
}