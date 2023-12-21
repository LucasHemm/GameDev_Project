using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor")]
public class Armor : Item
{
    public int defense;
    public int magicDefense;
    public int bonusHealth;
    public int bonusMana;

}
