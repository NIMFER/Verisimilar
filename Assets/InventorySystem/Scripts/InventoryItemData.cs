using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OpenCodeBox/Inventory/Item Data")]
public class InventoryItemData : ScriptableObject
{

    // Item type
    public string id; // Item Id
    public string displayName; // Item name
    public Texture preview;
    public GameObject prefab;

    // Conditions
    public int slot; // How much slot to take
    public float duration; // Duration for this effect

    // Effects
    public float speed; // Speed effect
    public float nightVision; // NightVision effect
    public float silence; // Silence effect
    public float stamina; // Stamina effect

}
