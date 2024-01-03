using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterUIScript : MonoBehaviour
{

    public Interact interact;
    public Button basicAttack;
    public Button ability1;
    public Button ability2;
    //make a textmeshpro text for 
    public TextMeshProUGUI statsheetText;
    public Image statSheetImage;

    public Image inventoryWeapon;
    public Image inventoryArmor;
    public Image inventory;
    public TextMeshProUGUI collectedGold;
    public GameController gameController;



    // Start is called before the first frame update
    void Start()
    {
        //get the Interact script from the gameobject main camera
        interact = GameObject.Find("Main Camera").GetComponent<Interact>();

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the interact selected character is not null
        if (interact.selectedCharacter != null && interact.selectedCharacter.hasAttacked == false)
        {
            // Set basic attack button to the sprite of the interact selected character weapon sprite
            if (interact.selectedCharacter.weapon.sprite != null)
            {
                basicAttack.GetComponent<Image>().sprite = interact.selectedCharacter.weapon.sprite;
            }
            if (interact.selectedCharacter.ability1.sprite != null)
            {
                ability1.GetComponent<Image>().sprite = interact.selectedCharacter.ability1.sprite;
            }
            if (interact.selectedCharacter.ability2.sprite != null)
            {
                ability2.GetComponent<Image>().sprite = interact.selectedCharacter.ability2.sprite;
            }
            if (interact.selectedCharacter.armor.sprite != null)
            {
                inventoryArmor.GetComponent<Image>().sprite = interact.selectedCharacter.armor.sprite;
            }
            if (interact.selectedCharacter.weapon.sprite != null)
            {
                inventoryWeapon.GetComponent<Image>().sprite = interact.selectedCharacter.weapon.sprite;
            }
            // Set the text of the stat sheet to the interact selected character's stats with strength agility and intellect    
            statsheetText.text = "Name: " + interact.selectedCharacter.characterClass.className + "\n"
                                + "Current Health: " + interact.selectedCharacter.currentHealth + "\n"
                                + "Max Health: " + interact.selectedCharacter.maxHealth + "\n"
                                + "Physical Defense: " + interact.selectedCharacter.armor.defense + "\n"
                                + "Magical Defense: " + interact.selectedCharacter.armor.magicDefense + "\n"
                                + "Strength: " + interact.selectedCharacter.characterClass.strength + "\n"
                                + "Agility: " + interact.selectedCharacter.characterClass.agility + "\n"
                                + "Intellect: " + interact.selectedCharacter.characterClass.intellect + "\n";


            // Enable the button

            // Check if the selected character is moving
            if (interact.selectedCharacter.Moving == true)
            {
                basicAttack.gameObject.SetActive(false);
                ability1.gameObject.SetActive(false);
                ability2.gameObject.SetActive(false);
                statSheetImage.gameObject.SetActive(false);
                inventoryArmor.gameObject.SetActive(false);
                inventoryWeapon.gameObject.SetActive(false);
                inventory.gameObject.SetActive(false);
            }
            else
            {
                basicAttack.gameObject.SetActive(true);
                // Check if uses on ability1 and ability2 is above 0
                if (interact.selectedCharacter.ability1.currentUses > 0)
                {
                    ability1.gameObject.SetActive(true);
                }
                else
                {
                    ability1.gameObject.SetActive(false);
                }
                if (interact.selectedCharacter.ability2.currentUses > 0)
                {
                    ability2.gameObject.SetActive(true);
                }
                else
                {
                    ability2.gameObject.SetActive(false);
                }
                statSheetImage.gameObject.SetActive(true);
                inventoryArmor.gameObject.SetActive(true);
                inventoryWeapon.gameObject.SetActive(true);
                inventory.gameObject.SetActive(true);
            }
        }
        else
        {
            // Set a default sprite or do nothing if there's no selected character
            // basicAttack.GetComponent<Image>().sprite = [Your Default Sprite];

            // Disable the button
            basicAttack.gameObject.SetActive(false);
            ability1.gameObject.SetActive(false);
            ability2.gameObject.SetActive(false);
            statSheetImage.gameObject.SetActive(false);
            inventoryArmor.gameObject.SetActive(false);
            inventoryWeapon.gameObject.SetActive(false);
            inventory.gameObject.SetActive(false);
        }
        collectedGold.text = "Gold: " + gameController.data.collectedGold;
    }
}
