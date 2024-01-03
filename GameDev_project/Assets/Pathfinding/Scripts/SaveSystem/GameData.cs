using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int xpGained;
    public List<string> unlockedCharacters;
    public Difficulty selectedDifficulty;
    public int stagesCompleted;

    public GameData(int xp, List<string> characters, Difficulty difficulty)
    {
        xpGained = xp;
        unlockedCharacters = characters;
        selectedDifficulty = difficulty;
        stagesCompleted = 0;
    }
}
