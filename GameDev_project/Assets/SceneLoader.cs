using UnityEngine;

public class SceneLoader : MonoBehaviour
{
   // Loads Generated Scene
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GeneratedScene");
    }

    //Loads shop scene
    public void LoadShop()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("ShopScene");
    }

    //Loads Rest scene
    public void LoadRest()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RestScene");
    }

    //Loads Choice scene   
    public static void LoadChoice()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ChoiceScene");
    }
}
