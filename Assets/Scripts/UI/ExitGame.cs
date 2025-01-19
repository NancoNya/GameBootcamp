using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
           SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
       }
}
