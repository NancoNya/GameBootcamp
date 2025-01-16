
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public string speaker;  // 说话者（例如 A, B, 旁白）
    public string content;  // 对话内容
}

[Serializable]
public class Chapter
{
    public string title;  // 章节标题
    public List<DialogueLine> lines;  // 本章节的所有对话
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Script")]
public class DialogueScript : ScriptableObject
{
    public List<Chapter> chapters;  // 所有章节的列表
}

