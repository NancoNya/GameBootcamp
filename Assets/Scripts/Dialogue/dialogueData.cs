using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueDataAsset", menuName = "Dialogue System/Dialogue Data")]
public class DialogueDataAsset : ScriptableObject
{
    public List<DialogueData> dialogueDatas = new List<DialogueData>();
}

[System.Serializable]
public class DialogueData
{
    public string Flag;
    public int ID;
    public string Character;
    public string Content;
    public string Next;
}

