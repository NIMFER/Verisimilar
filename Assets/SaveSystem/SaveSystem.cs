using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    // Save data as binary format file
    public static void saveData(InventoryData inventory, int index)
    {
        if (SavedSaveSystem.loadData().savedFile.Length > 0)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string path = Application.persistentDataPath + SavedSaveSystem.loadData().savedFile[index] + ".dfile"; // dfile is my own file type which basically means DataFile
            FileStream stream = new FileStream(path, FileMode.Create);

            Data data = new Data(inventory);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    // Load from data as binary format file and return, if not found then inform
    public static Data loadData(int index)
    {
        if (SavedSaveSystem.loadData().savedFile.Length > 0)
        {
            string path = Application.persistentDataPath + SavedSaveSystem.loadData().savedFile[index] + ".dfile";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                Data data = formatter.Deserialize(stream) as Data;
                stream.Close();

                return data;
            }
            else
            {
                Debug.Log("FILE NOT FOUND!");
                return null;
            }
        } else
        {
            Debug.Log("SAVED FILES NOT FOUND!");
            return null;
        }
    }

    public static void addData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + SavedSaveSystem.loadData().savedFile[SavedSaveSystem.loadData().savedFile.Length] + ".dfile"; // dfile is my own file type which basically means DataFile
        FileStream stream = new FileStream(path, FileMode.Create);
        stream.Close();
    }

    public static void removeData(int index)
    {
        if (SavedSaveSystem.loadData().savedFile.Length > 0)
        {
            string path = Application.persistentDataPath + SavedSaveSystem.loadData().savedFile[index] + ".dfile";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

}
