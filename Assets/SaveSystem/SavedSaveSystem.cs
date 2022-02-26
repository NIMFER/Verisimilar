using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavedSaveSystem
{

    // Save data as binary format file
    public static void saveData(string[] name)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/SavedSaveFile.dfile"; // dfile is my own file type which basically means DataFile
        FileStream stream = new FileStream(path, FileMode.Create);

        SavedSaveFiles data = new SavedSaveFiles(name);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Load from data as binary format file and return, if not found then inform
    public static SavedSaveFiles loadData()
    {
        string path = Application.persistentDataPath + "/SavedSaveFile.dfile";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedSaveFiles data = formatter.Deserialize(stream) as SavedSaveFiles;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("FILE NOT FOUND!");
            return null;
        }
    }

}
