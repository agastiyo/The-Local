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
    private bool rayHit, uiLoaded; //Did the ray hit something?/Is the ui already loaded?
    private GameObject obj, prevObj; //the object the raycast hit in this/previous frame
    private RaycastHit hit, prevHit; //data of what the raycast hit in this/previous frame

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
        dialogueHandler = FindObjectOfType<DialogueHandler>();
        actionHandler = FindObjectOfType<ActionHandler>();
        itemHandler = FindObjectOfType<ItemHandler>();

        center = new Vector3(Screen.width / 2, Screen.height / 2);

        Ray ray = Camera.main.ScreenPointToRay(center);
        Physics.Raycast(ray, out prevHit, rayDist);
        prevObj = prevHit.transform.gameObject;

        uiLoaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(center);
        rayHit = Physics.Raycast(ray, out hit, rayDist);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        obj = hit.transform.gameObject;

        if (rayHit && prevObj != obj) //if the player looks toward a new object
        {
            Debug.Log("This object is different!");

            if (obj.GetComponent<NPCProfile>()) //if the object is an npc
            {
                dialogueHandler.SetCurrentGraph(obj.GetComponent<NPCProfile>().dialogue);
                actionHandler.EnableTalking();
                if (!uiLoaded) { levelHandler.Load("Action"); }

                Debug.Log("Graph has been set!");
                uiLoaded = true;
            }
            else if (obj.GetComponent<ItemObject>()) //if the object is a pickupable
            {
                itemHandler.SetCurrentItem(obj.GetComponent<ItemObject>());
                actionHandler.EnablePickups();
                if (!uiLoaded) { levelHandler.Load("Action"); }

                Debug.Log("Item has been set!");
                uiLoaded = true;
            }
            else //if the object doesnt matter
            {
                ResetAll();
            }
        }
        else if (rayHit && obj == prevObj) { } //nothing
        else { ResetAll(); } //reset everything

        prevObj = obj;
    }

    void ResetAll() 
    {
        dialogueHandler.SetCurrentGraph(null);
        itemHandler.SetCurrentItem(null);

        actionHandler.DisableTalking();
        actionHandler.DisablePickups();

        levelHandler.Unload("Action");

        Debug.Log("Graph/Item has been nulled!");

        uiLoaded = false;
    }
}
