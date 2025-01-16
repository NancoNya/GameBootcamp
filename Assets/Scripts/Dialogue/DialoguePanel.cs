using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    public Image characterLeft;
    public Image characterRight;
    public TextMeshProUGUI dialogueContanter;

    public float PlayWordSpeed;
    private float PlayWordTimer;
    
    private string text = "";
    private int curWord;
    public bool dialogueEnd { get; private set; }

    public float JumpTimer = .1f;
    public float JumpCounter;

    private void Start()
    {
        dialogueEnd = true;
    }

    private void Update()
    {
        if (PlayWordSpeed > 0)
        {
            PlayWordSpeed -= Time.deltaTime;
        }

        if (JumpCounter > 0)
        {
            JumpCounter -= Time.deltaTime;
        }
        
        if (text.Length > dialogueContanter.text.Length && curWord < text.Length && PlayWordSpeed < 0)
        {
            dialogueContanter.text += text[curWord ++ ].ToString();
            dialogueEnd = false;
            PlayWordSpeed = PlayWordTimer;
        }
        else if(text.Length <= dialogueContanter.text.Length && curWord >= text.Length)
        {
            dialogueEnd = true;
            JumpCounter = JumpTimer;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && JumpCounter < 0)
        {
            dialogueContanter.text = text;
            curWord = text.Length;
        }
    }

    public void SetImage(Sprite _sprite1, Sprite _sprite2)
    {
        characterLeft.sprite = _sprite1;
        characterRight.sprite = _sprite2;
    }

    public void SetImage()
    {
        characterLeft.sprite = null;
        characterRight.sprite = null;
    }

    public void PlayDialogue(string dialogue, float playeSpeed)
    {
        dialogueContanter.text = "";
        curWord = 0;
        text = dialogue;
        PlayWordSpeed = playeSpeed;
        PlayWordTimer = playeSpeed;
        dialogueEnd = false;
    }

    public void HideCharacter()
    {
        characterLeft.color = Color.black;
        characterRight.color = Color.black;
    }

    public void ShowCharacterRight()
    {
        characterLeft.color = Color.black;
        characterRight.color = Color.white;
    }
    
    public void ShowCharacterLeft()
    {
        characterLeft.color = Color.white;
        characterRight.color = Color.black;
    }
}
