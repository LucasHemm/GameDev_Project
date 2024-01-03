using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public Button WarriorButton;
    public Button MageButton;
    public Button RangerButton;

    public Button backButton;

    public List<Button> itemButtons;

    public Armor armor;

    public List<Weapon> weapons;

    public GameObject itemBackground;

    public CharacterData data;

    public TextMeshProUGUI goldText;

    string currentClassName;




    void Start()
    {
        data = ReadAndWrite.loadFromJson();
        goldText.text = "Gold: " + data.collectedGold.ToString();

        //Set all buttons to active
        WarriorButton.gameObject.SetActive(true);
        MageButton.gameObject.SetActive(true);
        RangerButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);

        //Set all item buttons to inactive
        foreach (Button button in itemButtons)
        {
            button.gameObject.SetActive(false);
        }
        itemBackground.SetActive(false);
        
    }

    public void ShowOnlyWarriorButton()
    {
        WarriorButton.gameObject.SetActive(true);
        MageButton.gameObject.SetActive(false);
        RangerButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        currentClassName = "Warrior";

        //Set all item buttons to active
        foreach (Button button in itemButtons)
        {
            button.gameObject.SetActive(true);
        }
        itemBackground.SetActive(true);
    }

    public void ShowOnlyMageButton()
    {
        WarriorButton.gameObject.SetActive(false);
        MageButton.gameObject.SetActive(true);
        RangerButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        currentClassName = "Mage";

        //Set all item buttons to active    
        foreach (Button button in itemButtons)
        {
            button.gameObject.SetActive(true);
        }
        itemBackground.SetActive(true);
    }

    public void ShowOnlyRangerButton()
    {
        WarriorButton.gameObject.SetActive(false);
        MageButton.gameObject.SetActive(false);
        RangerButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        currentClassName = "Ranger";

        //Set all item buttons to active
        foreach (Button button in itemButtons)
        {
            button.gameObject.SetActive(true);
        }
        itemBackground.SetActive(true);
    }

    public void BackButtonClick()
    {
        WarriorButton.gameObject.SetActive(true);
        MageButton.gameObject.SetActive(true);
        RangerButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        currentClassName = "";

        //Set all item buttons to inactive
        foreach (Button button in itemButtons)
        {
            button.gameObject.SetActive(false);
        }
        itemBackground.SetActive(false);
    }

    public void BuyArmor()
    {
        CharacterData characterData = ReadAndWrite.loadFromJson();

        if (characterData.collectedGold >= armor.price)
        {
            characterData.collectedGold -= armor.price;
            int index = characterData.characterClassNames.IndexOf(currentClassName);
            characterData.armors[index] = armor;
            ReadAndWrite.SaveToJson(characterData);
            goldText.text = "Gold: " + characterData.collectedGold.ToString();
        }
    }

    public void BuyWeapon(int weaponIndex)
    {
        CharacterData data = ReadAndWrite.loadFromJson();
        if(data.collectedGold >= weapons[weaponIndex].price)
        {
            data.collectedGold -= weapons[weaponIndex].price;
            int index = data.characterClassNames.IndexOf(currentClassName);
            data.weapons[index] = weapons[weaponIndex];
            ReadAndWrite.SaveToJson(data);
            goldText.text = "Gold: " + data.collectedGold.ToString();
        }        
    }
}
