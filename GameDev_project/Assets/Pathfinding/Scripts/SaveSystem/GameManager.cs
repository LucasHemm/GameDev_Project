using UnityEngine;
using System.Collections.Generic;

public enum UnlockableClasses
{
    Warrior,
    Mage,
    Ranger
}

public class GameManager : MonoBehaviour
{
    private GameData gameData;

    private void Start()
    {
        gameData = SaveSystem.LoadGame();

        if (gameData == null)
        {
            //If no saved data, initialize with default values
            gameData = new GameData(0, new List<string> { "Warrior" }, Difficulty.Easy);
            SaveSystem.SaveGame(gameData);
        }

        SimulateGameplayEvents();

        DisplayGameState();
    }

    private void SimulateGameplayEvents()
    {
        //player actions or events that affect game state
        GainXP(150);
        PurchaseCharacter(UnlockableClasses.Mage);

        CompleteStage(Difficulty.Easy, 3);
        CompleteStage(Difficulty.Medium, 3);
    }

    private void GainXP(int amount)
    {
        //gaining XP based on player actions
        gameData.xpGained += amount;

        SaveSystem.SaveGame(gameData);
    }

    private void PurchaseCharacter(UnlockableClasses unlockableClasses)
{
    // Purchasing a class with XP
    int cost = GetCharacterCost(unlockableClasses);

    // Check if the class is not already unlocked and if the player has enough XP
    if (gameData.xpGained >= cost && !gameData.unlockedCharacters.Contains(unlockableClasses.ToString()))
    {
        gameData.xpGained -= cost;
        gameData.unlockedCharacters.Add(unlockableClasses.ToString());
        Debug.Log(unlockableClasses + " class purchased!");
    }

    SaveSystem.SaveGame(gameData);
}


    private void CompleteStage(Difficulty difficulty, int stagesCompleted)
    {
        //completing stages to unlock higher difficulties
        if (gameData.selectedDifficulty == difficulty && gameData.stagesCompleted >= stagesCompleted)
        {
            UnlockNextDifficulty();
        }
    }

    private void UnlockNextDifficulty()
    {
        //unlocking the next difficulty
        Difficulty nextDifficulty = gameData.selectedDifficulty + 1;

        if (nextDifficulty <= Difficulty.Nightmare)
        {
            gameData.selectedDifficulty = nextDifficulty;
            Debug.Log(nextDifficulty + " difficulty unlocked!");
        }

        SaveSystem.SaveGame(gameData);
    }

    private int GetCharacterCost(UnlockableClasses unlockableClasses)
    {
        //XP cost for each character 
        switch (unlockableClasses)
        {
            case UnlockableClasses.Warrior:
                return 200;
            case UnlockableClasses.Mage:
                return 200;
            case UnlockableClasses.Ranger:
                return 200;
            default:
                return 0;
        }
    }

    private void DisplayGameState()
    {
        //current game state in the console
        Debug.Log("XP Gained: " + gameData.xpGained);
        Debug.Log("Unlocked Classes: " + string.Join(", ", gameData.unlockedCharacters));
        Debug.Log("Selected Difficulty: " + gameData.selectedDifficulty);
    }
}
