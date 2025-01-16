using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public Transform positionToGo;
    public bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
            Debug.Log("send player to BossScene");
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadSceneAsync("BossScene", LoadSceneMode.Additive).completed += OnBossSceneLoaded;
        }
    }

    private void OnBossSceneLoaded(AsyncOperation asyncOperation)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = positionToGo.position;
        }
        // 设置 BossScene 为激活场景
        Scene bossScene = SceneManager.GetSceneByName("BossScene");
        SceneManager.SetActiveScene(bossScene);
    }
}
