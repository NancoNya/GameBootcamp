using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueDataImporter : MonoBehaviour
{
    public DialogueDataAsset[] dialogueDataAssets;
    public DialoguePanel _dialoguePanel;
    public DialogueScript _dialogueScript;
    public GameObject background;
    public GameObject enemy;

    public Sprite black;
    public Sprite me;
    public Sprite A;
    public Sprite B;

    public Sprite me1;
    public Sprite me2;
    public Sprite me3;
    public Sprite me4;
    public Sprite me5;
    
    private int dialogueDataAssetIndex = 0;
    private int index = 0;
    public float playSpeed;
    public bool TheFirst = true;
    
    public bool ProglueEnd = false;
    public int linesIndex;

    public bool dialogueEnd;

    private void Start()
    {
        _dialoguePanel.SetImage();
        dialogueEnd = true;
    }

    private void Update()
    {
        dialogueDataAssetIndex = DialogueTrigger.Instance.dialogueIndex;
        ProglueEnd = DialogueTrigger.Instance.ProdialogueEnd;
        
        if (ProglueEnd && _dialoguePanel.dialogueEnd)
            SetDialogue();
        else if (_dialoguePanel.dialogueEnd)
        {
            PlayProglue();
        }
    }

    /// <summary>
    /// 针对序章的播放
    /// </summary>
    public void PlayProglue()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || TheFirst) && !ProglueEnd && _dialogueScript.chapters[0].lines[linesIndex].content != "旁白" && _dialoguePanel.dialogueEnd)
        {
            stop();
            dialogueEnd = false;
            background.SetActive(true);
            _dialoguePanel.SetImage(A, B);
            if (_dialogueScript.chapters[0].lines[linesIndex].speaker == "A") _dialoguePanel.ShowCharacterLeft();
            else if(_dialogueScript.chapters[0].lines[linesIndex].speaker == "B") _dialoguePanel.ShowCharacterRight();
            else
            {
                _dialoguePanel.HideCharacter();
            }
            
            _dialoguePanel.PlayDialogue(_dialogueScript.chapters[0].lines[linesIndex].content, playSpeed);
            linesIndex++;
            TheFirst = false;
        }
        else if (_dialogueScript.chapters[0].lines[linesIndex].content == "旁白" && _dialoguePanel.dialogueEnd)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                play();
                dialogueEnd = true;
                ProglueEnd = true;
                TheFirst = true;
                DialogueTrigger.Instance.ProdialogueEnd = true;
                GameInput.RefreshAllButtons();
                background.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 针对后续剧情的播放
    /// </summary>
    public void SetDialogue()
    {
        //Debug.Log(index);
        if (dialogueDataAssets[dialogueDataAssetIndex].dialogueDatas[index].Flag == "@" && _dialoguePanel.dialogueEnd && (Input.GetKeyDown(KeyCode.Space) || TheFirst))
        {
            stop();
            
            dialogueEnd = false;
            _dialoguePanel.SetImage(me, black);
            if (dialogueDataAssets[dialogueDataAssetIndex].dialogueDatas[index].Character == "Black") _dialoguePanel.ShowCharacterRight();
            else if (dialogueDataAssets[dialogueDataAssetIndex].dialogueDatas[index].Character == "Me") _dialoguePanel.ShowCharacterLeft();
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
            if (dialogueDataAssetIndex == 0) DialogueTrigger.Instance.dialogue0 = true;
            else if (dialogueDataAssetIndex == 1) DialogueTrigger.Instance.dialogue1 = true;
            else if (dialogueDataAssetIndex == 2) DialogueTrigger.Instance.dialogue2 = true;
            else if (dialogueDataAssetIndex == 3) DialogueTrigger.Instance.dialogue3 = true;
            else if (dialogueDataAssetIndex == 4) DialogueTrigger.Instance.dialogue4 = true;
            else if (dialogueDataAssetIndex == 5) DialogueTrigger.Instance.dialogue5 = true;
            else if (dialogueDataAssetIndex == 6) DialogueTrigger.Instance.dialogue6 = true;
            else if (dialogueDataAssetIndex == 7) DialogueTrigger.Instance.dialogue7 = true;
            
            //退出
            if (Input.GetKeyDown(KeyCode.Space))
            {
                play();
               
                index = 0;
                dialogueEnd = true;
                TheFirst = true;
                GameInput.RefreshAllButtons();
                gameObject.SetActive(false);
            }
        }
    }

    public void stop()
    {
        AudioListener.volume = 0;
        enemy.SetActive(false);
    }

    public void play()
    {
        AudioListener.volume = 1f;
        enemy.SetActive(true);
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

