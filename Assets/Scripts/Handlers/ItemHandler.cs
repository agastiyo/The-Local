using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [HideInInspector]
    public ItemObject currentItem;

    public void SetCurrentItem(ItemObject item) 
    {
        currentItem = item;
    }
}
