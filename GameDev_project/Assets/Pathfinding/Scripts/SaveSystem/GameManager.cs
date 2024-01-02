using UnityEngine;
using System.Collections.Generic;

public enum CharacterClass
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
        PurchaseCharacter(CharacterClass.Mage);

        CompleteStage(Difficulty.Easy, 3);
        CompleteStage(Difficulty.Medium, 3);
    }

    private void GainXP(int amount)
    {
        //gaining XP based on player actions
        gameData.xpGained += amount;

        SaveSystem.SaveGame(gameData);
    }

    private void PurchaseCharacter(CharacterClass characterClass)
    {
        //purchasing a class with XP
        int cost = GetCharacterCost(characterClass);

        if (gameData.xpGained >= cost && !gameData.unlockedCharacters.Contains(characterClass.ToString()))
        {
            gameData.xpGained -= cost;
            gameData.unlockedCharacters.Add(characterClass.ToString());
            Debug.Log(characterClass + " class purchased!");
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

    private int GetCharacterCost(CharacterClass characterClass)
    {
        //XP cost for each character 
        switch (characterClass)
        {
            case CharacterClass.Warrior:
                return 200;
            case CharacterClass.Mage:
                return 200;
            case CharacterClass.Ranger:
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
