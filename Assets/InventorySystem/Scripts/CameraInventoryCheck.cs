using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTTSC_Character_Controller_V2.Core.Scripts;

public class CameraInventoryCheck : MonoBehaviour
{
    private PlayerInputReceiver playerInputReceiver;
    public InventoryData Inventory;
    public float pickupDistance;
    RaycastHit hitData;
    int layerMask;

    private void OnEnable()
    {
        playerInputReceiver = FindObjectOfType<PlayerInputReceiver>();
        playerInputReceiver.InteractInputEvent += Interact;
    }

    void Interact(float performed)
    {
        // Create ray
        Ray ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray, out hitData, pickupDistance, layerMask);
    }

    void FixedUpdate()
    {

        // Bit shift the index of the layer (10) to get a bit mask
        layerMask = 1 << 10;

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
