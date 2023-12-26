using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterUIScript : MonoBehaviour
{

    public Interact interact;
    public Button basicAttack;



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
    if(interact.selectedCharacter != null)
    {
        // Set basic attack button to the sprite of the interact selected character weapon sprite
        basicAttack.GetComponent<Image>().sprite = interact.selectedCharacter.weapon.sprite;

        // Enable the button
        basicAttack.gameObject.SetActive(true);
    }
    else
    {
        // Set a default sprite or do nothing if there's no selected character
        // basicAttack.GetComponent<Image>().sprite = [Your Default Sprite];

        // Disable the button
        basicAttack.gameObject.SetActive(false);
    }
}
}
