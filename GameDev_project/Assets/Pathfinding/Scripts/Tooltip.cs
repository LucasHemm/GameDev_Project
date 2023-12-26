using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string type;
    private string message;

    public Interact interact;

    private void CreateMessage()
    {
        Debug.Log("CreateMessage****************");

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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter****************");

        CreateMessage();
        TooltipManager.instance.SetandShowTooltip(message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit****************");

        TooltipManager.instance.HideTooltip();
    }
}
