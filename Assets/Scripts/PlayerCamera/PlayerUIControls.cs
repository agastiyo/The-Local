using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIControls : MonoBehaviour
{
    private ActionHandler actionHandler;

    public Canvas inventory;
    public Canvas pauseMenu;

    private bool uiEnabled;

    // Start is called before the first frame update
    void Start()
    {
        actionHandler = FindObjectOfType<ActionHandler>();

        inventory.enabled = false;
        pauseMenu.enabled = false;
        uiEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(uiEnabled) 
        {
            case false:
                if (Input.GetKeyDown(KeyCode.Tab)) 
                {
                    inventory.enabled = true;
                    uiEnabled = true;
                    actionHandler.DisableControls();
                    Time.timeScale = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    pauseMenu.enabled = true;
                    uiEnabled = true;
                    actionHandler.DisableControls();
                    Time.timeScale = 0;
                }
                break;
            case true:
                if (Input.GetKeyDown(KeyCode.Tab) && !pauseMenu.enabled) 
                {
                    inventory.enabled = false;
                    uiEnabled = false;
                    actionHandler.EnableControls();
                    Time.timeScale = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Escape) && !inventory.enabled) 
                {
                    pauseMenu.enabled = false;
                    uiEnabled = false;
                    actionHandler.EnableControls();
                    Time.timeScale = 1;
                }
                break;
        }
    }
}
