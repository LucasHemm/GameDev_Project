using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int totalXP;
    public int startingGold;
    public int totalGold;
    public int warriorLevel;
    public int mageLevel;
    public int rangerLevel;
    public List<Difficulty> difficulties;
    public int levelsCleared;

    public GameData(int totalXP, int startingGold, int totalGold, int warriorLevel, int mageLevel, int rangerLevel, List<Difficulty> difficulties, int levelsCleared)
    {
        this.totalXP = totalXP;
        this.startingGold = startingGold;
        this.totalGold = totalGold;
        this.warriorLevel = warriorLevel;
        this.mageLevel = mageLevel;
        this.rangerLevel = rangerLevel;
        this.difficulties = difficulties;
        this.levelsCleared = levelsCleared;
    }
}
