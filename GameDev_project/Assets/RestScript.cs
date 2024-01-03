using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestScript : MonoBehaviour
{
    public void Rest()
    {
        CharacterData data = ReadAndWrite.loadFromJson();
        for (int i = 0; i < data.currentHealths.Count; i++)
        {
            data.currentHealths[i] = getMaxFromClassName(data.characterClassNames[i]);
        }
        ReadAndWrite.SaveToJson(data);        
    }

    private int getMaxFromClassName(string className)
    {
        switch (className)
        {
            case "Warrior":
                return 130;
            case "Mage":
                return 100;
            case "Rogue":
                return 110;
            default:
                return 100;
        }
    }


}
