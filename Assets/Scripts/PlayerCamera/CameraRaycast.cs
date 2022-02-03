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
    private bool rayHit; //did the ray hit something?
    private RaycastHit hit; //data of what the raycast hit
    private bool prevRayHit; //did the ray hit something last frame?
    private RaycastHit prevHit; //data of what the raycast hit in the last frame
    private GameObject focused; //NPC/Item hit by current raycast
    private bool isLooking; //If the player is alreadly looking at someting

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
        dialogueHandler = FindObjectOfType<DialogueHandler>();
        actionHandler = FindObjectOfType<ActionHandler>();
        itemHandler = FindObjectOfType<ItemHandler>();

        center = new Vector3(Screen.width / 2, Screen.height / 2);

        Ray ray = Camera.main.ScreenPointToRay(center);
        prevRayHit = Physics.Raycast(ray, out prevHit, rayDist);

        isLooking = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(center);
        rayHit = Physics.Raycast(ray, out hit, rayDist);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        GameObject prevObj = prevHit.transform.gameObject; //not being set idk this is the problem
        GameObject obj = hit.transform.gameObject;

        if (rayHit && prevObj != obj) 
        //if the player looks toward a new object
        {
            Debug.Log("This object is different!");

            if (hit.transform.gameObject.GetComponent<NPCProfile>()) //if the object is an npc
            {
                focused = obj;

                dialogueHandler.SetCurrentGraph(focused.GetComponent<NPCProfile>().dialogue);
                actionHandler.EnableTalking();
                levelHandler.Load("Action");

                Debug.Log("Graph has been set!");
                isLooking = true;
            }
            else if (hit.transform.gameObject.GetComponent<ItemObject>()) //if the object is a pickupable
            {
                focused = obj;

                itemHandler.SetCurrentItem(focused.GetComponent<ItemObject>());
                actionHandler.EnablePickups();
                levelHandler.Load("Action");

                Debug.Log("Item has been set!");
                isLooking = true;
            }
            else //if the object doesnt matter
            {
                ResetAll();
            }
        }
        else if (rayHit && obj == prevObj) { } //nothing
        else { ResetAll(); } //reset everything

        prevRayHit = rayHit;
    }

    void ResetAll() 
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
