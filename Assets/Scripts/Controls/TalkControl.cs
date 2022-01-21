using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkControl : MonoBehaviour
{
    private LevelHandler levelHandler;

    [HideInInspector]
    public bool inDialogue;

    private void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
        inDialogue = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inDialogue)
        {
            levelHandler.Load("Dialogue");
            inDialogue = true;
            //Note: unload command in NodeReader.cs
        }
    }
}
