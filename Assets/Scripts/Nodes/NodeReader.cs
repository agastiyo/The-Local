using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using TMPro;

public class NodeReader : MonoBehaviour
{
    [HideInInspector]
    public DialogueGraph graph; //dialogue to use
    public TextMeshProUGUI npcName; //name of the npc
    public TextMeshProUGUI npcDialogue; //what the npc says

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
            if(node.GetString() == "Start") //if this is the starting node
            {
                graph.current = node; //graph should start there
                Debug.Log("Set current node!");
                break;
            }
        }

        StartCoroutine(ParseNode());
    }

    private IEnumerator ParseNode() 
    {
        Debug.Log("Parsing node!");
        string[] nodeData = graph.current.GetString().Split("/"); //get node data

        if (nodeData[0] == "Start") //if this node is the start node
        {
            NextNode("exit"); //move on
        }
        if (nodeData[0] == "DialogueNode") //if its a dialogue node
        {
            //Process the dialogue
            npcName.text = nodeData[1];
            npcDialogue.text = nodeData[2];
            //wait for mouse click
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            //move on
            NextNode("exit");
        }
        if (nodeData[0] == "Exit") //if this is the last node
        {
            actionHandler.InDialogue(false); //no longer in a dialogue
            levelHandler.Unload("Dialogue");
            actionHandler.EnableControls();
            actionHandler.EnableTalking();
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
