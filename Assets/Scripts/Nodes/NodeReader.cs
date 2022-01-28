using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using TMPro;

public class NodeReader : MonoBehaviour
{
    [HideInInspector]
    public DialogueGraph graph;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI npcDialogue;

    private LevelHandler levelHandler;
    private DialogueHandler dialogueHandler;
    private ActionHandler actionHandler;

    private void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
        dialogueHandler = FindObjectOfType<DialogueHandler>();
        actionHandler = FindObjectOfType<ActionHandler>();

        graph = dialogueHandler.currentGraph;
        Debug.Log("Current Graph loaded in reader!");

        foreach (BaseNode node in graph.nodes)
        {
            if(node.GetString() == "Start") 
            {
                graph.current = node;
                Debug.Log("Set current node!");
                break;
                //identify starting node
            }
        }

        StartCoroutine(ParseNode());
    }

    private IEnumerator ParseNode() 
    {
        Debug.Log("Parsing node!");
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
            actionHandler.InDialogue(false); //no longer in a dialogue
            levelHandler.Unload("Dialogue");
            actionHandler.EnableControls();
            //Load command in TalkControl.cs
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
