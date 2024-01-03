using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//create asset menu for persistence between scenes
[CreateAssetMenu(fileName = "PersistenceBetweenScenes", menuName = "PersistenceBetweenScenes")]
public class PersistenceBetweenScenes : ScriptableObject
{

    //create list of characters of length 3
    //public List<Character> characters = new List<Character>(3);
    //list of charcater classes
    public List<Armor> armors = new List<Armor>(3);
    public List<Weapon> weapons = new List<Weapon>(3);
    public List<int> currentHealths = new List<int>(3);
    public List<string> characterClassNames = new List<string>(3);
    public int levelsCleared;





    
}
