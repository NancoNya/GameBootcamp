
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class DialogueTrigger : MonoSigleton<DialogueTrigger>
{
    public int dialogueIndex;
    public bool ProdialogueEnd = false;

    public bool dialogue0;
    public bool dialogue1;
    public bool dialogue2;
    public bool dialogue3;
    public bool dialogue4;
    public bool dialogue5;
    public bool dialogue6;
    public bool dialogue7;

    public bool CanPlayDialogue2;

    public GameObject DialogueObject;
    public DialogueDataImporter dialogueImporter;

    private void Start()
    {
        dialogueImporter = DialogueObject.GetComponent<DialogueDataImporter>();
        PlayDialogue1();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (dialogueImporter.dialogueEnd && !dialogue6)
            {
                dialogueIndex = 6;
                PlayDialogue1();
            }
            else if (dialogueImporter.dialogueEnd && !dialogue7)
            {
                dialogueIndex = 7;
                PlayDialogue1();
            }
        }
        //Debug.Log(dialogue0);
        if (dialogueImporter.dialogueEnd && !dialogue0)
        {
            dialogueIndex = 0;
            PlayDialogue1();
        }
        else if (dialogueImporter.dialogueEnd && !dialogue1)
        {
            dialogueIndex = 1;
            PlayDialogue1();
        }
        /*else if (dialogue0 && dialogue1)
        {
            DialogueObject.SetActive(false);
        }*/

        if (CanPlayDialogue2)
        {
            if (dialogueImporter.dialogueEnd && !dialogue2)
            {
                dialogueIndex = 2;
                PlayDialogue2();
            }
            else if (dialogueImporter.dialogueEnd && !dialogue4)
            {
                dialogueIndex = 4;
                PlayDialogue2();
            }
            else if (dialogueImporter.dialogueEnd && !dialogue5)
            {
                dialogueIndex = 5;
                PlayDialogue2();
            }
        }
    }

    public void PlayDialogue1()
    {
        if (dialogueIndex <= 7)
        {
           
            DialogueObject.SetActive(true);
            GameInput.ConsumeAllButtons();
        }
    }

    public void PlayDialogue2()
    {
        if (dialogueIndex <= 7)
        {
            DialogueObject.SetActive(true);
            GameInput.ConsumeAllButtons();
        }
    }
    
}
