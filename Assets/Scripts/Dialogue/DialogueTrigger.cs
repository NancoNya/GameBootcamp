<<<<<<< HEAD
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
=======
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
>>>>>>> 8fea9370cd45b9a79d606ad84cc0afef5db5eef5
