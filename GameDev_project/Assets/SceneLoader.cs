using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    public static CharacterData characterData;
    public static GameData gameData;



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
    public static string GetCurrentSceneName()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
    public static void LoadEnd()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
    }

    public static void LoadMenuAndReset()
    {
        gameData = SaveSystem.LoadGame();
        characterData = ReadAndWrite.loadFromJson();
        gameData.totalXP += characterData.collectedXP;
        gameData.totalGold += characterData.collectedGold;
        if(characterData.levelsCleared > gameData.levelsCleared) 
        {
            gameData.levelsCleared = characterData.levelsCleared;
        }
        SaveSystem.SaveGame(gameData);
        ReadAndWrite.deleteJson();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public static void LoadUpgrade()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UpgradeScene");
    }

    public static void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
