using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialoguer : MonoBehaviour
{
    public DialogueGraph thisDialogue;

    private TalkControl talkControl;
    private PlayerControls playerControls;
    private PlayerCameraController playerCameraController;
    private DialogueHandler dialogueHandler;

    // Start is called before the first frame update
    void Start()
    {
        talkControl = FindObjectOfType<TalkControl>();
        playerControls = FindObjectOfType<PlayerControls>();
        playerCameraController = FindObjectOfType<PlayerCameraController>();
        dialogueHandler = FindObjectOfType<DialogueHandler>();
        talkControl.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        talkControl.enabled = true;
        dialogueHandler.currentGraph = thisDialogue;
        Debug.Log("Graph has been set!");
    }

    private void OnTriggerExit(Collider other)
    {
        talkControl.enabled = false;
        dialogueHandler.currentGraph = null;
        Debug.Log("Graph has been nulled!");
    }
}
