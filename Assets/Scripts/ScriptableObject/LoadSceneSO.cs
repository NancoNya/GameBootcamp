using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Event/LoadSceneSO")]
public class LoadSceneSO : ScriptableObject
{
    [SerializeField]public  SaveData DefaultPlayerData = new SaveData();
    [SerializeField]public  SaveData PlayerData = new SaveData();
    
    //玩家选择读取存档
    // public void LoadFromPlayerPrefs()
    // {
    //     
    //     // var json=SaveSystem.LoadFromPlayerPrefs("PlayerData");
    //     // Debug.Log(json);
    //     // if (json != "")
    //     // {
    //     //     var savaData = JsonUtility.FromJson<SaveData>(json);
    //     //     
    //     //     PlayerData.PlayerPosition = savaData.PlayerPosition;
    //     //     PlayerData.PlayerCurrentHealth = savaData.PlayerCurrentHealth;
    //     // }
    //     // else
    //     // {
    //     //     NewGame();
    //     // }
    // }
    
    //玩家选择开启新游戏，数据恢复成默认数据
    public void NewGame()
    {
        //PlayerPrefs.DeleteKey("PlayerData");
        
        PlayerData.PlayerPosition=DefaultPlayerData.PlayerPosition;
        PlayerData.PlayerCurrentHealth = DefaultPlayerData.PlayerCurrentHealth;
    }
    
}
   
