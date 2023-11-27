using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuListener : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject optionsMenu;

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
            pauseButton.SetActive(!pauseButton.activeSelf);
            if(optionsMenu.activeSelf){
            menu.SetActive(false);
            }
            optionsMenu.SetActive(false);
        }
        
    }
}
