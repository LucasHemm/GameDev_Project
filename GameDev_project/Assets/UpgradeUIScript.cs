using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUIScript : MonoBehaviour
{

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI highScoreText;
    public GameData gameData;
    public Button upgradeGold;
    public Button upgradeDifficulty;


    void Start()
    {
        gameData = SaveSystem.LoadGame();
        goldText.text = gameData.totalGold.ToString();
        xpText.text = gameData.totalXP.ToString();   
        foreach(Difficulty difficulty in gameData.difficulties)
        {
            if(difficulty.unlocked == true)
            {
                difficultyText.text = "Unlock Difficulty: " + difficulty.difficultyName;
            }
        }
        highScoreText.text = "High Score: " + gameData.levelsCleared;
    }

    public void UpgradeGold()
    {
        if(gameData.totalXP >= 100)
        {
            gameData.totalXP -= 100;
            gameData.startingGold += 10;
            xpText.text = "Total XP:" + gameData.totalXP;
            SaveSystem.SaveGame(gameData);
        }
    }

    public void UpgradeDifficulty()
    {
        if(gameData.totalXP >= 200)
        {
            gameData.totalXP -= 200;
            foreach(Difficulty difficulty in gameData.difficulties)
            {
                if(difficulty.unlocked == false)
                {
                    difficulty.unlocked = true;
                    difficultyText.text = "Difficulty: " + difficulty.difficultyName;
                    return;
                }
            }
            SaveSystem.SaveGame(gameData);
        }
    }
}
