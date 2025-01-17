
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoSigleton<DialogueTrigger>
{
    public int dialogueIndex;
    public bool ProdialogueEnd = false;

    public GameObject UI;

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E) && dialogueIndex <= 7)
        {
            GameInput.ConsumeAllButtons();
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            if (ProdialogueEnd)
                StartCoroutine(AddDialogueIndex());
        }*/
    }

    public void PlayDialogue()
    {
        if (dialogueIndex <= 7)
        {
            GameInput.ConsumeAllButtons();
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            if (ProdialogueEnd)
                StartCoroutine(AddDialogueIndex());
        }
    }

    private IEnumerator AddDialogueIndex()
    {
        yield return new WaitForSecondsRealtime(1f);
        dialogueIndex++;
    }
}
