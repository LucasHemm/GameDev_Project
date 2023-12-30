using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public string description;
    public Sprite sprite;
    public int range;
    public int minDamage;
    public int maxDamage;
    public string damageType;
    public string modifier;
    public int uses;    




    public int manaCost;

    
}

