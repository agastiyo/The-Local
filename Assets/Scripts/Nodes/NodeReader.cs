using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class NodeReader : MonoBehaviour
{
    [HideInInspector]
    public DialogueGraph graph;

    private TextMesh npcName;
    private TextMesh npcDialogue;
    private LevelHandler levelHandler;
    private TalkControl talkControl;
    private DialogueHandler dialogueHandler;

    private void Start()
    {
        GameObject.Find("Speaker").TryGetComponent(out npcName);
        GameObject.Find("Dialogue").TryGetComponent(out npcDialogue);

        levelHandler = FindObjectOfType<LevelHandler>();
        talkControl = FindObjectOfType<TalkControl>();
        dialogueHandler = FindObjectOfType<DialogueHandler>();

        graph = dialogueHandler.currentGraph;
        

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
        if (nodeData[0] == "Exit")
        {
            talkControl.inDialogue = false;
            levelHandler.Unload("Dialogue");
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
