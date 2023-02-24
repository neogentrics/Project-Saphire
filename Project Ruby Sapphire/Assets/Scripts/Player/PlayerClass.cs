using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public enum UnlockedList { Swordsman, Archer, Enchanter, Marauder, Puglilist };
    public enum LockedClasses { Warrior, Gladiator, Mage, Bard, Monk };
    public enum AdvancedLockedClasses { DualWielder, DarkKnight, MageKnight, DarkMage, LightMage, ArcaneArcher, MysticMonk };

    public UnlockedList currentClass;
    public List<LockedClasses> lockedClasses;
    public List<AdvancedLockedClasses> advancedLockedClasses;

    private int maxLevel = 150;

    public void SetClass(UnlockedList newClass)
    {
        currentClass = newClass;
    }

    public bool IsUnlocked(UnlockedList targetClass)
    {
        return targetClass <= currentClass;
    }

    public bool IsLocked(LockedClasses targetClass)
    {
        return lockedClasses.Contains(targetClass);
    }

    public bool IsAdvancedLocked(AdvancedLockedClasses targetClass)
    {
        return advancedLockedClasses.Contains(targetClass);
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }

    public int CalculateXPForLevel(int level)
    {
        float baseXP = 100f;
        return Mathf.RoundToInt(baseXP * Mathf.Pow(level, 1.5f));
    }

    public void UnlockClass(LockedClasses newClass)
    {
        if (lockedClasses.Contains(newClass))
        {
            lockedClasses.Remove(newClass);
        }
    }

    public void UnlockAdvancedClass(AdvancedLockedClasses newClass)
    {
        if (advancedLockedClasses.Contains(newClass))
        {
            advancedLockedClasses.Remove(newClass);
        }
    }
}
