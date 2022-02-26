using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedSaveFiles
{

    public string[] savedFile;

    public SavedSaveFiles(string[] name)
    {
        savedFile = new string[name.Length];
        for (int i = 0; i < name.Length; i++)
        {
            savedFile[i] = name[i];
        }
    }

}
