using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Datatypes we want to save to the binary format,
// Only C# datatype works such as string, int, float etc
[System.Serializable]
public class Data
{

    // Create variables we want
    public string[] inventoryId;
    public float[] playerPosition;

    // Save data to the variables
    public Data(InventoryData inventory)
    {
        inventoryId = new string[3];
        playerPosition = new float[3];

        for (int i = 0; i < inventoryId.Length; i++)
        {
            inventoryId[i] = inventory.ItemList[i].id;
        }

        playerPosition[0] = 0;
        playerPosition[1] = 0;
        playerPosition[2] = 0;
    }

}
