using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialoguer : MonoBehaviour
{
    public DialogueGraph thisDialogue;

    private LevelHandler level;
    private DialogueHandler dialogue;
    private ActionHandler action;

    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<LevelHandler>();
        dialogue = FindObjectOfType<DialogueHandler>();
        action = FindObjectOfType<ActionHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        action.EnableTalking();
        dialogue.SetCurrentGraph(thisDialogue);
        level.Load("Action");
        Debug.Log("Graph has been set!");
    }

    private void OnTriggerExit(Collider other)
    {
        action.DisableTalking();
        dialogue.ClearCurrentGraph();
        level.Unload("Action");
        Debug.Log("Graph has been nulled!");
    }
}
