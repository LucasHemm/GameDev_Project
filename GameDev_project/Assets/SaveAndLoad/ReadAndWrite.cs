using System.IO;
using System.Collections.Generic;
using UnityEngine;


public class ReadAndWrite : MonoBehaviour
{
    public List<int> currentHealths = new List<int>(3);
    public List<string> characterClassNames = new List<string>(3);
    public List<Armor> armors = new List<Armor>(3);
    public List<Weapon> weapons = new List<Weapon>(3);
    public int levelsCleared;

    public static void SaveToJson(CharacterData newData)
    {

        Debug.Log("Saving to json");
        CharacterData data = newData;
        string json = JsonUtility.ToJson(data,true);
        File.WriteAllText(Application.dataPath + "/CharacterSavedData.json", json);
    }

    public static CharacterData loadFromJson()
    {
        Debug.Log("Loading from json");
        string json = File.ReadAllText(Application.dataPath + "/CharacterSavedData.json");
        CharacterData data = JsonUtility.FromJson<CharacterData>(json);

        return data;

    }

    public static void deleteJson()
    {
        Debug.Log("Deleting json");
        File.Delete(Application.dataPath + "/CharacterSavedData.json");
    }
}
