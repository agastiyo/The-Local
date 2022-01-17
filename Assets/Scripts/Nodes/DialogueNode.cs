using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    public string npcName;
    public string npcDialogue;

    public override string GetString()
    {
        return $"DialogueNode/{npcName}/{npcDialogue}";
    }
}
