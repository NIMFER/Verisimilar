using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OpenCodeBox/Inventory/Item Data")]
public class InventoryItemData : ScriptableObject
{

    public string id; // Item Id
    public string displayName; // Item name
    public GameObject prefab;

    //public int slot; // How much slot you want to substract
    //public float duration; // Duration for this effect
    //public float speed; // Speed effect
    //public bool nightVision; // NightVision effect

}
