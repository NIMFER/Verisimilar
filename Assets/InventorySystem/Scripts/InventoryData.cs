using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryData : MonoBehaviour
{

    public InventoryItemData noneData; // used to set slot to none
    public InventoryItemData[] ItemList; // Items
    public RawImage[] Slot; // Slots

    bool toggleShowItems = true;

    // Update slot textures
    void updateSlot(int Index, Texture preview)
    {
        Slot[Index].texture = preview;
    }

    // Toggle show items
    void toggleShow()
    {
        toggleShowItems = !toggleShowItems;
        gameObject.SetActive(toggleShowItems);
    }

    // Get item from list
    public InventoryItemData getItem(int Index)
    {
        return ItemList[Index];
    }

    // Drop item from list
    public void dropItem(int Index)
    {
        if (ItemList[Index].id != "0")
        {
            // dropCode(ItemList[Index]) <- Add something like that later
            ItemList[Index] = noneData;
            updateSlot(Index, ItemList[Index].preview);
        }
    }

    // Save item to the list
    public void storeItem(int Index, InventoryItemData value)
    {
        ItemList[Index] = value;
        updateSlot(Index, ItemList[Index].preview);
    }

}