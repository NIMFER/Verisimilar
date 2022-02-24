using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInventoryCheck : MonoBehaviour
{

    public InventoryData Inventory;
    public float pickupDistance;

    void FixedUpdate()
    {

        // Bit shift the index of the layer (10) to get a bit mask
        int layerMask = 1 << 10;

        // Create ray
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData, pickupDistance, layerMask);

        // Check if any collider found
        if (hitData.collider != null)
        {
            // If collider is item
            if (hitData.collider.tag == "Item")
            {
                // Check if item is not air or none
                if (int.Parse(hitData.collider.gameObject.GetComponent<ItemBehaviour>().ItemData.id) > 0)
                {
                    // Save item and check if item can be stored
                    InventoryItemData Item = hitData.collider.gameObject.GetComponent<ItemBehaviour>().ItemData;
                    if (Inventory.storeItem(Item))
                    {
                        // Destroy if inventory has free slot
                        Destroy(hitData.collider.gameObject);
                    }
                }
            }
            else if (hitData.collider.tag == "Door")
            {
                hitData.collider.gameObject.GetComponent<DoorBehaviour>().interactDoor("closeOpen");
            }
        }
    }

}
