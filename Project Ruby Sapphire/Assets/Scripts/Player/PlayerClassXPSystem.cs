using UnityEngine;

public class PlayerClassXPSystem : MonoBehaviour
{
    public int baseXP = 100;
    public int maxLevel = 150;

    public int GetXPForLevel(int level)
    {
        return Mathf.RoundToInt(baseXP * Mathf.Pow(level, 1.5f));
    }

    public int GetMaxXP()
    {
        return GetXPForLevel(maxLevel);
    }
}


/*

This script exposes a public variable baseXP, which represents the base amount of XP required for the first level of a class. It also exposes a public variable maxLevel, which represents the maximum level a class can reach.

The script provides two public methods:

GetXPForLevel(int level) takes an integer argument level and returns the amount of XP required to reach that level using the formula Exp = Base * Level^1.5. The Mathf.Pow method is used to calculate Level^1.5.
GetMaxXP() returns the amount of XP required to reach the maximum level, which is equal to GetXPForLevel(maxLevel).
You can use this script to calculate the amount of XP required for each level of a class and to check if a player has reached the maximum level.

*/