using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [Range(0f,20f)]
    public float rayDist; //the distance the raycast detects objects

    private DialogueHandler dialogueHandler;
    private ActionHandler actionHandler;
    private LevelHandler levelHandler;
    private ItemHandler itemHandler;

    private Vector3 center; //center of the screen
    private RaycastHit hit; //data of what the raycast hit
    private GameObject focused; //NPC/Item hit by raycast
    private bool isLooking; //If the player is alreadly looking at someting

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
        bool rayHit = Physics.Raycast(ray, out hit, rayDist);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if (rayHit && !isLooking) //if the player looks toward an object
        {
            if (hit.transform.gameObject.GetComponent<NPCProfile>()) //if the object is an npc
            {
                focused = hit.transform.gameObject;

                dialogueHandler.SetCurrentGraph(focused.GetComponent<NPCProfile>().dialogue);
                actionHandler.EnableTalking();
                levelHandler.Load("Action");

                Debug.Log("Graph has been set!");
                isLooking = true;
            }
            else if (hit.transform.gameObject.GetComponent<ItemObject>()) //if the object is a pickupable
            {
                focused = hit.transform.gameObject;

                itemHandler.SetCurrentItem(focused.GetComponent<ItemObject>());
                actionHandler.EnablePickups();
                levelHandler.Load("Action");

                Debug.Log("Item has been set!");
                isLooking = true;
            }
        }
        else if (!rayHit && isLooking) //if the player looks away from an object
        {
            focused = null;

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
