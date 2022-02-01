using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupControl : MonoBehaviour
{
    private ItemHandler itemHandler;

    // Start is called before the first frame update
    void Start()
    {
        itemHandler = FindObjectOfType<ItemHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            itemHandler.currentItem.OnHandlePickupItem();
        }
    }
}
