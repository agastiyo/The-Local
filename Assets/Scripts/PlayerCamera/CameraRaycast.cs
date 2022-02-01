using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    private DialogueHandler dialogueHandler;
    private ActionHandler actionHandler;
    private LevelHandler levelHandler;

    private Vector3 center;
    private RaycastHit hit;
    private NPCProfile focusedObject;
    private bool isLooking;

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
        dialogueHandler = FindObjectOfType<DialogueHandler>();
        actionHandler = FindObjectOfType<ActionHandler>();

        center = new Vector3(Screen.width / 2, Screen.height / 2);
        isLooking = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(center);
        bool rayHit = Physics.Raycast(ray, out hit, 7);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if (rayHit && !isLooking)
        {
            if (hit.transform.gameObject.GetComponent<NPCProfile>()) 
            {
                focusedObject = hit.transform.gameObject.GetComponent<NPCProfile>();
                dialogueHandler.SetCurrentGraph(focusedObject.dialogue);
                actionHandler.EnableTalking();
                levelHandler.Load("Action");
                Debug.Log("Graph has been set!");
            }
            //add else if the ray hits a pickupable object

            isLooking = true;
        }
        else if (!rayHit && isLooking)
        {
            focusedObject = null;
            dialogueHandler.SetCurrentGraph(null);
            actionHandler.DisableTalking();
            levelHandler.Unload("Action");
            Debug.Log("Graph has been nulled!");

            isLooking = false;
        }
    }
}
