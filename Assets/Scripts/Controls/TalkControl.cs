using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkControl : MonoBehaviour
{
    private LevelHandler levelHandler;
    private ActionHandler actionHandler;

    [HideInInspector]
    public bool inDialogue;

    private void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
        actionHandler = FindObjectOfType<ActionHandler>();
        inDialogue = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inDialogue)
        {
            actionHandler.DisableControls();
            levelHandler.Load("Dialogue");
            inDialogue = true;
            //Note: unload command in NodeReader.cs
        }
    }
}
