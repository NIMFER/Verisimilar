using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedSaveManage : MonoBehaviour
{

    public void addSave(string name)
    {
        string[] Files;
        if (SavedSaveSystem.loadData() != null && SavedSaveSystem.loadData().savedFile.Length != 0)
        {
            Files = new string[SavedSaveSystem.loadData().savedFile.Length + 1];
            for (int i = 0; i < Files.Length-1; i++)
            {
                Files[i] = SavedSaveSystem.loadData().savedFile[i];
            }
            Files[Files.Length-1] = name;
        }
        else
        {
            Files = new string[1];
            Files[0] = name;
        }
        SavedSaveSystem.saveData(Files);
        GameObject.Find("SaveSystem").GetComponent<SaveData>().save(Files.Length - 1);
    }

    public void removeSave(int index)
    {
        string[] Files = SavedSaveSystem.loadData().savedFile;
        string[] newFiles = new string[0];
        if (Files.Length > 0)
        {
            newFiles = new string[Files.Length - 1];
            int fileIndex = 0;
            for (int i = 0; i < Files.Length; i++)
            {
                if (i != index)
                {
                    newFiles[fileIndex] = Files[i];
                    fileIndex++;
                }
            }
        }
        SaveSystem.removeData(index);
        SavedSaveSystem.saveData(newFiles);
    }

    public string loadSave(int index)
    {
        return SavedSaveSystem.loadData().savedFile[index];
    }

}
