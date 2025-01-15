using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    [Header("Input File Path (relative to StreamingAssets)")]
    public string fileName = "ACT文本.txt";  // 文件名
    public DialogueScript dialogueAsset;    // 用于保存解析后的 ScriptableObject

    private void Start()
    {
        ParseFile();
    }

    public void ParseFile()
    {
        string filePath = "E:/UnityProgram/GameBoot/Assets/ACT文本.txt";
        Debug.Log(filePath);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found: {filePath}");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);  // 逐行读取文件内容
        Chapter currentChapter = null;

        dialogueAsset.chapters = new List<Chapter>();

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            // 检查是否是章节标题
            if (line.StartsWith("Chapter"))
            {
                if (currentChapter != null)
                    dialogueAsset.chapters.Add(currentChapter);

                currentChapter = new Chapter
                {
                    title = line,
                    lines = new List<DialogueLine>()
                };
            }
            else if (line.Contains(":"))  // 检测到对话
            {
                string[] parts = line.Split(new[] { ':' }, 2);
                string speaker = parts[0].Trim();
                string content = parts[1].Trim();

                currentChapter?.lines.Add(new DialogueLine { speaker = speaker, content = content });
            }
            else  // 如果是旁白
            {
                currentChapter?.lines.Add(new DialogueLine { speaker = "旁白", content = line.Trim() });
            }
        }

        if (currentChapter != null)
            dialogueAsset.chapters.Add(currentChapter);

        Debug.Log("File parsed successfully!");
    }
}