using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    public GameObject doorPivotRotation;
    public GameObject doorRotation;
    public float degree;
    Quaternion target;

    string idItem = "5";

    public bool locked = true;
    bool closed = true;

    InventoryData Inventory;

    // Start is called before the first frame update
    void Start()
    {
        Inventory = GameObject.Find("InventorySystem").GetComponent<InventoryData>();
    }

    void Update()
    {
        doorPivotRotation.transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 150.0f);
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

        } 
        else if (Event == "closeOpen")
        {
            for (int i = 0; i < Inventory.ItemList.Length; i++)
            {
                if (Inventory.ItemList[i].id == idItem)
                {
                    if (closed && !locked)
                    {
                        closed = false;
                        target = Quaternion.Euler(doorRotation.transform.eulerAngles);
                        break;
                    }
                    else
                    {
                        closed = true;
                        target = Quaternion.Euler(0, doorRotation.transform.eulerAngles.y + degree, 0);
                        break;
                    }
                }
            }
        }
    }

}