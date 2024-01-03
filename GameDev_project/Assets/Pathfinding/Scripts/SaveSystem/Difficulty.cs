using System;

[System.Serializable]

public class Difficulty
{
    public string difficultyName;
    public int xpCost;
    public bool unlocked;

    public Difficulty(string difficultyName, int xpCost, bool unlocked)
    {
        this.difficultyName = difficultyName;
        this.xpCost = xpCost;
        this.unlocked = unlocked;
    }
}