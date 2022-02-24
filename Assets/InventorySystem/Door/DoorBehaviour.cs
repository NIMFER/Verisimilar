using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    string idItem = "5";

    public bool locked = true;
    bool closed = true;

    InventoryData Inventory;

    // Start is called before the first frame update
    void Start()
    {
        Inventory = GameObject.Find("InventorySystem").GetComponent<InventoryData>();
    }

    public void interactDoor(string Event)
    {
        if (Event == "lockUnlock")
        {
            if (closed)
            {
                for (int i = 0; i < Inventory.ItemList.Length; i++)
                {
                    if (Inventory.ItemList[i].id == idItem)
                    {
                        locked = !locked;
                    }
                }
            }
            Debug.Log(locked);
        } 
        else if (Event == "closeOpen")
        {
            for (int i = 0; i < Inventory.ItemList.Length; i++)
            {
                if (Inventory.ItemList[i].id == idItem)
                {
                    if (closed && !locked)
                    {
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        gameObject.SetActive(true);
                    }
                }
            }
        }
    }

}