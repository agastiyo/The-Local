using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraRaycast : MonoBehaviour
{
    [Range(0f,20f)]
    public float rayDist; //the distance the raycast detects objects
    public Canvas actionUI; //the action ui
    
    private DialogueHandler dialogueHandler;
    private ActionHandler actionHandler;
    private ItemHandler itemHandler;

    private Vector3 center; //center of the screen
    private TextMeshProUGUI actionText; //text in action UI
    private GameObject obj, prevObj; //the object the raycast hit in this/previous frame
    private RaycastHit hit, prevHit; //data of what the raycast hit in this/previous frame

    // Start is called before the first frame update
    void Start()
    {
        actionUI.enabled = false;

        dialogueHandler = FindObjectOfType<DialogueHandler>();
        actionHandler = FindObjectOfType<ActionHandler>();
        itemHandler = FindObjectOfType<ItemHandler>();

        center = new Vector3(Screen.width / 2, Screen.height / 2);

        actionText = actionUI.GetComponentInChildren<TextMeshProUGUI>();

        Ray ray = Camera.main.ScreenPointToRay(center);
        Physics.Raycast(ray, out prevHit, rayDist);
        prevObj = prevHit.transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(center);
        Physics.Raycast(ray, out hit, rayDist);

        obj = hit.transform.gameObject;

        if (obj != prevObj) //if the player looks toward a new object
        {
            Debug.Log("This object is different!");

            if (obj.GetComponent<NPCProfile>()) //if the object is an npc
            {
                dialogueHandler.SetCurrentGraph(obj.GetComponent<NPCProfile>().dialogue);
                actionHandler.EnableTalking();
                actionText.text = "E - Talk";
                actionUI.enabled = true;

                Debug.Log("Graph has been set!");
            }
            else if (obj.GetComponent<ItemObject>()) //if the object is a pickupable
            {
                itemHandler.SetCurrentItem(obj.GetComponent<ItemObject>());
                actionHandler.EnablePickups();
                actionText.text = $"E - Pick Up {obj.GetComponent<ItemObject>().referenceItem.displayName}";
                actionUI.enabled = true;

                Debug.Log("Item has been set!");
            }
            else //if the object doesnt matter
            {
                dialogueHandler.SetCurrentGraph(null);
                itemHandler.SetCurrentItem(null);

                actionHandler.DisableTalking();
                actionHandler.DisablePickups();

                actionUI.enabled = false;

                Debug.Log("Graph/Item has been nulled!");
            }
        }

        prevObj = obj;
    }
}
