using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public Transform positionToGo;
    public bool isTriggered = false;

    // 修正方法名
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
            Debug.Log("send player to BossScene");
            // 先加载 Boss 场景
            SceneManager.LoadSceneAsync("BossScene", LoadSceneMode.Single).completed += OnBossSceneLoaded;
        }
    }

    private void OnBossSceneLoaded(AsyncOperation asyncOperation)
    {
        // 寻找 Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = positionToGo.position;
        }
        else
        {
            Debug.LogError("Player not found in BossScene");
        }
        // 设置 BossScene 为激活场景
        Scene bossScene = SceneManager.GetSceneByName("BossScene");
        SceneManager.SetActiveScene(bossScene);
        // 卸载当前场景
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}