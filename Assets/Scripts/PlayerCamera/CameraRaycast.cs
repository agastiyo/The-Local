using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [Range(0f,20f)]
    public float rayDist; //the distance the raycast detects objects
    public Canvas actionUI;

    private DialogueHandler dialogueHandler;
    private ActionHandler actionHandler;
    private ItemHandler itemHandler;

    private Vector3 center; //center of the screen
    private bool rayHit; //Did the ray hit something?
    private GameObject obj, prevObj; //the object the raycast hit in this/previous frame
    private RaycastHit hit, prevHit; //data of what the raycast hit in this/previous frame

    // Start is called before the first frame update
    void Start()
    {
        dialogueHandler = FindObjectOfType<DialogueHandler>();
        actionHandler = FindObjectOfType<ActionHandler>();
        itemHandler = FindObjectOfType<ItemHandler>();

        center = new Vector3(Screen.width / 2, Screen.height / 2);

        Ray ray = Camera.main.ScreenPointToRay(center);
        Physics.Raycast(ray, out prevHit, rayDist);
        prevObj = prevHit.transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(center);
        rayHit = Physics.Raycast(ray, out hit, rayDist);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        obj = hit.transform.gameObject;

        if (rayHit && obj != prevObj) //if the player looks toward a new object
        {
            Debug.Log("This object is different!");

            if (obj.GetComponent<NPCProfile>()) //if the object is an npc
            {
                dialogueHandler.SetCurrentGraph(obj.GetComponent<NPCProfile>().dialogue);
                actionHandler.EnableTalking();
                actionUI.enabled = true;

                Debug.Log("Graph has been set!");
            }
            else if (obj.GetComponent<ItemObject>()) //if the object is a pickupable
            {
                itemHandler.SetCurrentItem(obj.GetComponent<ItemObject>());
                actionHandler.EnablePickups();
                actionUI.enabled = true;

                Debug.Log("Item has been set!");
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

        actionUI.enabled = false;

        Debug.Log("Graph/Item has been nulled!");
    }
}
