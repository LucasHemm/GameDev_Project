public enum Difficulty
{
    Easy,
    Medium,
    Nightmare
}

public static class DifficultyExtensions
{
    public static string GetDisplayName(this Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return "Easy";
            case Difficulty.Medium:
                return "Medium";
            case Difficulty.Nightmare:
                return "Nightmare";
            default:
                return difficulty.ToString(); // If an unknown difficulty is encountered, use its string representation
        }
    }
}

