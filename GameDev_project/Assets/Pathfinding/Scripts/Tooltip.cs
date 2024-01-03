using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string type;
    private string message;

    public Interact interact;

    public Armor armor;
    public Weapon weapon;


    private void CreateMessage()
    {

        // Make a method that makes it so if it's a basicAttack type it hovers over, it will create a message with its price, min and max dmg, its range, int type, and its description
        if (type == "basicAttack")
        {
            message = "Price: " + interact.selectedCharacter.weapon.price + "\n"
                    + "Min Damage: " + interact.selectedCharacter.weapon.minDamage + "\n"
                    + "Max Damage: " + interact.selectedCharacter.weapon.maxDamage + "\n"
                    + "Range: " + interact.selectedCharacter.weapon.range + "\n"
                    + "Type: " + interact.selectedCharacter.weapon.type + "\n"
                    + "Description: " + interact.selectedCharacter.weapon.description;
        }
        if(type == "ability1"){
            message = "abilityName: " + interact.selectedCharacter.ability1.abilityName + "\n"
                    + "Description: " + interact.selectedCharacter.ability1.description + "\n"
                    + "Range: " + interact.selectedCharacter.ability1.range + "\n"
                    + "Min Damage: " + interact.selectedCharacter.ability1.minDamage + "\n"
                    + "Max Damage: " + interact.selectedCharacter.ability1.maxDamage + "\n"
                    + "Damage Type: " + interact.selectedCharacter.ability1.damageType + "\n"
                    + "Modifier: " + interact.selectedCharacter.ability1.modifier + "\n"
                    + "Uses: " + interact.selectedCharacter.ability1.currentUses
                    + "/" + interact.selectedCharacter.ability1.maxUses;
        }
        if(type == "ability2"){
            message = "abilityName: " + interact.selectedCharacter.ability2.abilityName + "\n"
                    + "Description: " + interact.selectedCharacter.ability2.description + "\n"
                    + "Range: " + interact.selectedCharacter.ability2.range + "\n"
                    + "Min Damage: " + interact.selectedCharacter.ability2.minDamage + "\n"
                    + "Max Damage: " + interact.selectedCharacter.ability2.maxDamage + "\n"
                    + "Damage Type: " + interact.selectedCharacter.ability2.damageType + "\n"
                    + "Modifier: " + interact.selectedCharacter.ability2.modifier + "\n"
                    + "Uses: " + interact.selectedCharacter.ability2.currentUses
                    + "/" + interact.selectedCharacter.ability2.maxUses;
        }
        if(type == "armor"){
            message = "Item Name: " + armor.itemName + "\n" +
                    "Price: " + armor.price + "\n"
                    +  "Defense: " + armor.defense + "\n"
                    + "Magic Defense: " + armor.magicDefense + "\n"
                    + "Description: " + armor.description;
        }
        if(type == "weapon"){
            message = "Item Name: " + weapon.itemName + "\n" +
                    "Price: " + weapon.price + "\n"
                    +  "Min Damage: " + weapon.minDamage + "\n"
                    + "Max Damage: " + weapon.maxDamage + "\n"
                    + "Range: " + weapon.range + "\n"
                    + "Type: " + weapon.type + "\n"
                    + "Description: " + weapon.description;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        

        CreateMessage();
        TooltipManager.instance.SetandShowTooltip(message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        TooltipManager.instance.HideTooltip();
    }
}
