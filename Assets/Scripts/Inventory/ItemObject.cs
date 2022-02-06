using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    private InventorySystem inventory;

    void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();
    }

    public void OnHandlePickupItem() 
    {
        inventory.Add(referenceItem);
        Destroy(gameObject);
    }
}
