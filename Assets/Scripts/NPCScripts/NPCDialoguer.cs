using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialoguer : MonoBehaviour
{
    public DialogueGraph thisDialogue;

    private LevelHandler levelHandler;
    private DialogueHandler dialogueHandler;
    private ActionHandler actionHandler;

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
        dialogueHandler = FindObjectOfType<DialogueHandler>();
        actionHandler = FindObjectOfType<ActionHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        actionHandler.EnableTalking();
        dialogueHandler.currentGraph = thisDialogue;
        levelHandler.Load("Action");
        Debug.Log("Graph has been set!");
    }

    private void OnTriggerExit(Collider other)
    {
        actionHandler.DisableTalking();
        dialogueHandler.currentGraph = null;
        levelHandler.Unload("Action");
        Debug.Log("Graph has been nulled!");
    }
}
