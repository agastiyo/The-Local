using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    private DialogueHandler dialogueHandler;
    private ActionHandler actionHandler;
    private LevelHandler levelHandler;
    private ItemHandler itemHandler;

    private Vector3 center;
    private RaycastHit hit;
    private NPCProfile focusedNPC;
    private ItemObject focusedItem;
    private bool isLooking;

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
        dialogueHandler = FindObjectOfType<DialogueHandler>();
        actionHandler = FindObjectOfType<ActionHandler>();
        itemHandler = FindObjectOfType<ItemHandler>();

        center = new Vector3(Screen.width / 2, Screen.height / 2);
        isLooking = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(center);
        bool rayHit = Physics.Raycast(ray, out hit, 7);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);

        if (rayHit && !isLooking)
        {
            if (hit.transform.gameObject.GetComponent<NPCProfile>()) //if the object is an npc
            {
                focusedNPC = hit.transform.gameObject.GetComponent<NPCProfile>();

                dialogueHandler.SetCurrentGraph(focusedNPC.dialogue);
                actionHandler.EnableTalking();
                levelHandler.Load("Action");

                Debug.Log("Graph has been set!");
            }
            else if (hit.transform.gameObject.GetComponent<ItemObject>()) //if the object is a pickupable
            {
                focusedItem = hit.transform.gameObject.GetComponent<ItemObject>();

                itemHandler.SetCurrentItem(focusedItem);
                actionHandler.EnablePickups();
                levelHandler.Load("Action");

                Debug.Log("Item has been set!");
            }

            isLooking = true;
        }
        else if (!rayHit && isLooking)
        {
            focusedNPC = null;
            focusedItem = null;

            dialogueHandler.SetCurrentGraph(null);
            itemHandler.SetCurrentItem(null);

            actionHandler.DisableTalking();
            actionHandler.DisablePickups();

            levelHandler.Unload("Action");

            Debug.Log("Graph/Item has been nulled!");

            isLooking = false;
        }
    }
}
