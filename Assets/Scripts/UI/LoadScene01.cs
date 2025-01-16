using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene01 : MonoBehaviour
{
    public LoadSceneSO loadSceneSO;
    public Button button1;
    public Button button2;

    private void Awake()
    {
        
    }

    private void Start()
    {
        button1.onClick.AddListener(() =>
        {
            loadSceneSO.NewGame();
            SceneManager.LoadScene("QianMeng");
        });
        
        button2.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("QianMeng");
        });
    }
    
}
