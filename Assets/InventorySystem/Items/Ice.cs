using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{

    GameObject InventorySystem;
    public InventoryItemData[] Object;
    InventoryData Data;

    // Start is called before the first frame update
    void Start()
    {
        InventorySystem = GameObject.Find("InventorySystem");
        Data = InventorySystem.GetComponent<InventoryData>();

        for (int i = 0; i < Object.Length; i++)
        {
            Data.storeItem(Object[i]);
        }
    }

}
