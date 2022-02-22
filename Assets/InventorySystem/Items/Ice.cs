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

        Data.storeItem(0, Object[0]);
        Data.storeItem(1, Object[1]);
        Data.storeItem(2, Object[2]);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            InventoryItemData TMP = other.gameObject.GetComponent<InventoryItemData>();
            Data.storeItem(0, TMP);
            Debug.Log(TMP.id);
        }
    }
    

}
