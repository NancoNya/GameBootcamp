using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]public class SaveData
{
    public Vector3 PlayerPosition;
    public float PlayerCurrentHealth;
}
// [System.Serializable]public class BossData
// {
//     public Vector3 BossPosition;
//     public float BossCurrentHealth;
// }

public class PlayerData : MonoBehaviour
{
    [Header("存储玩家数据的数据盒")]
    public LoadSceneSO loadSceneSO;
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
        GameStart();
        
    }

    
    

    public void Save()
    {
        SaveByPlayerPrefs();
    }


    public void Load()
    {
        //LoadFromPlayerPrefs();
    }

    private void SaveByPlayerPrefs()
    {
        // SaveData saveData = new SaveData();
        //
        // saveData.PlayerPosition = transform.position;
        // saveData.PlayerCurrentHealth = character.currentHealth;
        //
        // SaveSystem.SaveByPlayerPrefs("PlayerData", saveData);
        
        loadSceneSO.PlayerData.PlayerPosition = transform.position;
        loadSceneSO.PlayerData.PlayerCurrentHealth = character.currentHealth;
        
    }
    // private void LoadFromPlayerPrefs()
    // {
    //     var json=SaveSystem.LoadFromPlayerPrefs("PlayerData");
    //     var savaData = JsonUtility.FromJson<SaveData>(json);
    //     
    //     transform.position = savaData.PlayerPosition;
    //     character.currentHealth = savaData.PlayerCurrentHealth;
    // }

    public void GameStart()
    {
        transform.position = loadSceneSO.PlayerData.PlayerPosition;
        character.currentHealth = loadSceneSO.PlayerData.PlayerCurrentHealth;
    }

    [UnityEditor.MenuItem("Developer/Delete Player Data Prefs")]
    public static void DeletePlayerDataPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
