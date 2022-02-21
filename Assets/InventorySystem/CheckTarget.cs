using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTTSC_Character_Controller_V2.Core.Scripts;

public class CheckTarget : MonoBehaviour
{
    PlayerInputReceiver playerInputReceiver;
    bool trigger = false;
    private void OnEnable()
    {
        playerInputReceiver = FindObjectOfType<PlayerInputReceiver>();

        playerInputReceiver.inventoryInputEvent += OpenInventory;
    }

    void OpenInventory(float value)
    {
        if (value == 1)
        {
            trigger = true;
        } else
        {
            trigger = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // When detected target and F pressed, destroy and save
        if (other.gameObject.tag == "ItemTarget" && trigger)
        {
            Destroy(gameObject);
        }
    }

}
