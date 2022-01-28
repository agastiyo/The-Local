using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerCameraController cameraController;
    private TalkControl talkControl;

    public void OnGameStart() 
    {
        playerControls = FindObjectOfType<PlayerControls>();
        cameraController = FindObjectOfType<PlayerCameraController>();
        talkControl = FindObjectOfType<TalkControl>();
        Debug.Log("Got player data!");

        playerControls.enabled = true;
        cameraController.enabled = true;
        talkControl.enabled = false;
    }

    public void EnableControls() 
    {
        playerControls.enabled = true;
        cameraController.enabled = true;
    }
    public void DisableControls() 
    {
        playerControls.enabled = false;
        cameraController.enabled = false;
    }
    public void EnableTalking() 
    { 
        talkControl.enabled = true; 
    }
    public void DisableTalking() 
    { 
        talkControl.enabled = false; 
    }

    public void InDialogue(bool inDialogue) 
    {
        talkControl.inDialogue = inDialogue;
    }
}
