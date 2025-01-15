using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoSigleton<DialogueTrigger>
{
    public int dialogueIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            StartCoroutine(AddDialogueIndex());
        }
    }

    private IEnumerator AddDialogueIndex()
    {
        yield return new WaitForSecondsRealtime(1f);
        dialogueIndex++;
    }
}
