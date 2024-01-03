using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(GameData gameData)
    {
        Debug.Log("Saving game");
        //save file to persistent data path, and save as binary
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/mlmgame.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        GameData gameData;
        string path = Application.dataPath + "/mlmgame.data";
        
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            gameData = formatter.Deserialize(stream) as GameData;
            stream.Close();

       return gameData;
    }

    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "/mlmgame.data";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}
