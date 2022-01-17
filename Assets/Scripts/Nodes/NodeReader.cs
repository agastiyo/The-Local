using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class NodeReader : MonoBehaviour
{
    public DialogueGraph graph;
    public TextMesh npcName;
    public TextMesh npcDialogue;

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

        StartCoroutine(ParseNode());
    }

    private IEnumerator ParseNode() 
    {
        string[] nodeData = graph.current.GetString().Split("/");
        if (nodeData[0] == "Start") 
        {
            NextNode("exit");
        }
        if (nodeData[0] == "DialogueNode")
        {
            //Process the dialogue
            npcName.text = nodeData[1];
            npcDialogue.text = nodeData[2];
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            NextNode("exit");
        }
    }

    public void NextNode(string fieldName) 
    {
        foreach (NodePort p in graph.current.Ports)
        {
            if (p.fieldName == fieldName) 
            {
                graph.current = p.Connection.node as BaseNode;
                break;
            }
        }

        StartCoroutine(ParseNode());
    }
}
