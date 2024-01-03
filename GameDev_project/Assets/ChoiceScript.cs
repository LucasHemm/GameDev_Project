using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceScript : MonoBehaviour
{
    public Button restButton;
    public Button shopButton;

    public void Start()
    {
        ShowRestButton();
        ShowShopButton();
        
    }

    private void ShowRestButton()
    {
        //Randomly show rest button
        if (Random.Range(0, 4) == 3)
        {
            restButton.gameObject.SetActive(true);
        }
        else
        {
            restButton.gameObject.SetActive(false);
        }
    }

    private void ShowShopButton()
    {
        //Randomly show shop button
        if (Random.Range(0, 2) == 1)
        {
            shopButton.gameObject.SetActive(true);
        }
        else
        {
            shopButton.gameObject.SetActive(false);
        }
    }
}
