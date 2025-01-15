using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicOpenAndClose : MonoBehaviour
{
    public AudioSource audioSource;
    public Sprite openMusic;
    public Sprite closeMusic;
    
    private Sprite imageSprite;
    private Button button;

    private void Awake()
    {
        imageSprite = GetComponent<Image>().sprite;
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(ChangeMusic);
    }
    

    public void ChangeMusic()
    {
        if (imageSprite == openMusic)
        {
           imageSprite = closeMusic;
           Debug.Log("切换成静音");
           audioSource.Stop();
        }
        else if(imageSprite==closeMusic)
        {
            imageSprite = openMusic;
            Debug.Log("打开声音");
            if(!audioSource.isPlaying)audioSource.Play();
        }
        GetComponent<Image>().sprite = imageSprite;
    }
}
