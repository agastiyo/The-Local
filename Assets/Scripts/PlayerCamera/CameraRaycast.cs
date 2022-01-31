using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    private DialogueHandler dialogueHandler;
    private ActionHandler actionHandler;
    private LevelHandler levelHandler;

    private Vector3 cameraCenter;
    private RaycastHit hit;
    private NPCDialoguer lookingAt;
    private bool isLooking;

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
        dialogueHandler = FindObjectOfType<DialogueHandler>();
        actionHandler = FindObjectOfType<ActionHandler>();

        cameraCenter =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
        isLooking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cameraCenter, transform.forward, out hit, 7) && !isLooking)
        {
            if (hit.transform.gameObject.GetComponent<NPCDialoguer>())
            {
                lookingAt = hit.transform.gameObject.GetComponent<NPCDialoguer>();
                dialogueHandler.SetCurrentGraph(lookingAt.thisDialogue);
                actionHandler.EnableTalking();
                levelHandler.Load("Action");
                Debug.Log("Graph has been set!");
            }
            isLooking = true;
        }
        else if (!Physics.Raycast(cameraCenter, transform.forward, out hit, 7) && isLooking)
        {
            dialogueHandler.SetCurrentGraph(null);
            actionHandler.DisableTalking();
            levelHandler.Unload("Action");
            Debug.Log("Graph has been nulled!");
            isLooking = false;
        }
    }
}
