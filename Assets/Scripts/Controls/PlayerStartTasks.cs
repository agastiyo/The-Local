using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartTasks : MonoBehaviour
{
    private ActionHandler actionHandler;
    
    void Start()
    {
        actionHandler = FindObjectOfType<ActionHandler>();

        actionHandler.OnGameStart();
    }
}
