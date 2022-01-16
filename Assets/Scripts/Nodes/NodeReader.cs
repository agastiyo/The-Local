using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class NodeReader : MonoBehaviour
{
    public DialogueGraph graph;

    private void Start()
    {
        foreach (BaseNode node in graph.nodes)
        {
            if(node.GetString() == "start") 
            {
                graph.current = node;
                break;
                //identify starting node
            }
        }
    }
}
