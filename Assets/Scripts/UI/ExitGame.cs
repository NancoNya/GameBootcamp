using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
       public Button quitButton;
   
       void Start()
       {
           quitButton.onClick.AddListener(QuitGame);
       }
   
       void QuitGame()
       {
           #if UNITY_EDITOR
               UnityEditor.EditorApplication.isPlaying = false;
           #else
               Application.Quit();
           #endif
       }
}
