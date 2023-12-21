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
        //set basic to be the sprite of the interct selected cahracter weapon sprite

        //check if the interact selected character is not null
        if(interact.selectedCharacter != null)
        {
            //set basic attack button to the sprite of the interact selected character weapon sprite
            basicAttack.GetComponent<Image>().sprite = interact.selectedCharacter.weapon.sprite;

        }
        
        //basicAttack.sprite = interact.selectedCharacter.weapon.sprite;
        //set basicattack source image to be the sprite of the interact selected character weapon sprite
        //basicAttack.GetComponent<BasicAttack>().sourceImage = interact.selectedCharacter.weapon.sprite;
        
    }
}
