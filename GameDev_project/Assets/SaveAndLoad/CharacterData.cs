using System.Collections.Generic;

[System.Serializable]
public class CharacterData
{
 public List<int> currentHealths = new List<int>(3);
 public List<string> characterClassNames = new List<string>(3);
 public int levelsCleared = 0;
 public List<Armor> armors = new List<Armor>(3);
 public List<Weapon> weapons = new List<Weapon>(3);
}