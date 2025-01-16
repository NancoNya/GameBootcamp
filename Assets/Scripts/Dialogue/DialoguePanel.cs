using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    public RawImage characterLeft;
    public RawImage characterRight;
    public TextMeshProUGUI dialogueContanter;

    public float PlayWordSpeed;
    private float PlayWordTimer;
    
    private string text = "";
    private int curWord;
    public bool dialogueEnd { get; private set; }

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
        
        if (text.Length > dialogueContanter.text.Length && curWord < text.Length && PlayWordSpeed < 0)
        {
            dialogueContanter.text += text[curWord ++ ].ToString();
            dialogueEnd = false;
            PlayWordSpeed = PlayWordTimer;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueContanter.text = text;
                curWord = text.Length;
            }
        }
        else if(text.Length <= dialogueContanter.text.Length && curWord >= text.Length)
        {
            dialogueEnd = true;
        }
    }

    public void SetImage(Texture _sprite1, Texture _sprite2)
    {
        characterLeft.texture = _sprite1;
        characterRight.texture = _sprite2;
    }

    public void SetImage()
    {
        characterLeft.texture = null;
        characterRight.texture = null;
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

    public void HideCharacterLeft()
    {
        characterLeft.color = Color.black;
        characterRight.color = Color.white;
    }
    
    public void HideCharacterRight()
    {
        characterLeft.color = Color.white;
        characterRight.color = Color.black;
    }
}
