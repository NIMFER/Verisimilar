using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{

    // Made this script to save easily data we want
    // and load easily data we want

    public InventoryData inventory;
    public SavedSaveManage saveManage;
    public int index; // Which file to load and save

    private string[] savedFile;

    void Start()
    {
        // TEST //
        //removefile(0);
        //addfile("nam");

        // If it says 0, then it means there are no save files
        Debug.Log("Amount save files: " + SavedSaveSystem.loadData().savedFile.Length);
        for (int i = 0; i < SavedSaveSystem.loadData().savedFile.Length; i++)
        {
            Debug.Log(SavedSaveSystem.loadData().savedFile[i]);
        }
    }

    // add save file
    public void addfile(string name)
    {
        bool free = true;
        for (int i = 0; i < SavedSaveSystem.loadData().savedFile.Length; i++)
        {
            if (SavedSaveSystem.loadData().savedFile[i] == name)
            {
                free = false;
            }
        }
        if (free)
        {
            saveManage.addSave(name);
        }
    }

    // read save file (Dont use this, it was used for testing)
    public void loadfile(int index)
    {
        Debug.Log(saveManage.loadSave(index));
    }

    // remove save file from list
    public void removefile(int index)
    {
        saveManage.removeSave(index);
    }

    // save data information
    public void save(int index)
    {
        SaveSystem.saveData(inventory, index);
    }

    // load data information and access it
    public Data load(int index)
    {
        if (SaveSystem.loadData(index) != null)
        {
            return SaveSystem.loadData(index);
        }
        else
        {
            return null;
        }
    }

}
