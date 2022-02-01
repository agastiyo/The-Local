using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    [HideInInspector]
    public DialogueGraph currentGraph { get; private set; }

    public void SetCurrentGraph(DialogueGraph graph)
    {
        currentGraph = graph;
    }
    public void ClearCurrentGraph()
    {
        currentGraph = null;
    }
}
