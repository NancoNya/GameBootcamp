using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class DialogueDataImporter : MonoBehaviour
{
    public DialogueDataAsset[] dialogueDataAssets;
    public DialoguePanel _dialoguePanel;

    public Texture black;
    public Texture me;
    
    private int dialogueDataAssetIndex = 0;
    private int index = 0;
    public float playSpeed;
    public bool TheFirst = true;

    private void Start()
    {
        _dialoguePanel.SetImage(black, me);
        dialogueDataAssetIndex = DialogueTrigger.Instance.dialogueIndex;
    }

    private void Update()
    {
        SetDialogue();
    }

    public void SetDialogue()
    {
        if (dialogueDataAssets[dialogueDataAssetIndex].dialogueDatas[index].Flag == "@" && _dialoguePanel.dialogueEnd && (Input.GetKeyDown(KeyCode.Space) || TheFirst))
        {
            if (dialogueDataAssets[dialogueDataAssetIndex].dialogueDatas[index].Character == "Black") _dialoguePanel.HideCharacterRight();
            else if (dialogueDataAssets[dialogueDataAssetIndex].dialogueDatas[index].Character == "Me") _dialoguePanel.HideCharacterLeft();
            else
            {
                _dialoguePanel.HideCharacter();
            }
            
            _dialoguePanel.PlayDialogue(dialogueDataAssets[dialogueDataAssetIndex].dialogueDatas[index].Content, playSpeed);
            
            index = int.Parse(dialogueDataAssets[dialogueDataAssetIndex].dialogueDatas[index].Next) - 1;
            TheFirst = false;
        }
        else if (dialogueDataAssets[dialogueDataAssetIndex].dialogueDatas[index].Flag == "end" && _dialoguePanel.dialogueEnd)
        {
            //退出
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.UnloadSceneAsync(1);
            }
        }
    }


    /*[MenuItem("Tools/Import Dialogue Data")]
    public static void ImportDialogueData()
    {
        // 加载 CSV 文件（假设放在 Resources 文件夹）
        TextAsset textData = Resources.Load<TextAsset>("Dialog/Chapter/Chapter1.7");
        if (textData == null)
        {
            Debug.LogError("Dialogue CSV file not found!");
            return;
        }

        // 解析 CSV 数据
        string[] data = textData.text.Split(new char[] { '\n' });
        DialogueDataAsset asset = ScriptableObject.CreateInstance<DialogueDataAsset>();

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            DialogueData q = new DialogueData
            {
                Flag = row[0],
                ID = int.Parse(row[1]),
                Character = row[2],
                Content = row[3],
                Next = row[4]
            };
            asset.dialogueDatas.Add(q);
        }

        // 保存为资产文件
        AssetDatabase.CreateAsset(asset, "Assets/ScriptObject/DialogueDataAsset8.asset");
        AssetDatabase.SaveAssets();

        Debug.Log("Dialogue Data Imported Successfully!");
    }*/
}

