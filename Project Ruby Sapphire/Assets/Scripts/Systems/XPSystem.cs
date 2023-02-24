using System;
using UnityEngine;

[System.Serializable]
public class XPSystem
{

    //Zmatias
    [Header("Added Variables : Current")]

    //This variable will store XP gained in current level 
    //and will restart when player is leveling app
    [Tooltip("XP that is gained on current level")]
    public int XPGainedDuringLevel;

    //This variable will calculated each time player levels up based on formule
    //This variable stores the XP amount that is needed for player to level up
    //From https://oldschool.runescape.wiki/w/Experience#Formula EXP_Diff

    [Tooltip("XP need to reach next level")]
    public int XPNeed4LevelUp;

    [Header("Added Variables : Totals")]

    //This variable which was originaly called experience is used to store Total EXP gained during runtime
    //From first level to Last.

    [Tooltip("Total Exp gained from beginning of runtime")]
    public int experience_Total;

    //This variable stores how much exp in Total is needed for next level
    //From https://oldschool.runescape.wiki/w/Experience#Formula Level
    [Tooltip("Total Exp needed for next level")]
    public int nextLevel_Xp_Total;
    //Zmatias



    [Header("RestVariables")]
    public int currentLevel;

    public Action OnLevelUp;

    public int MAX_EXP;
    public int MAX_LEVEL = 90;

    [System.NonSerialized]
    public int playerLevel;
    private int oldLevel;

    public XPSystem(int level, Action OnlevUp)
    {
        MAX_EXP = GetXPForLevel(MAX_LEVEL);
        currentLevel = level;

        //Zmatias
        //When System is constracted we should check which level is tha starting point
        //If player starts from level>1
        //We need to calculate the total experience that has gained from previous levels
        //Otherwise
        // it means that currently doesnt gained any XP since this is the first level
        //So we set experience total to 0    


        if (currentLevel > 1)
        {

            //Total experience is equal to experiance gained from previous level
            experience_Total = GetXPForLevel(currentLevel);
            Debug.Log("Current level greater than one" + experience_Total);
        }
        else
        {
            //if level is one NO exp has been gained yet.
            experience_Total = 0;
        }

        //Zmatias
        //Calculate total experience need for next level
        nextLevel_Xp_Total = GetXPForLevel(level + 1);

        //Zmatias
        //Calculate XP need to be gained for next level
        //by substracting from nextLevel Total that is need, current total that player has
        XPNeed4LevelUp = nextLevel_Xp_Total - experience_Total;

        //Zmatias
        //Player hasnt gained any experience during this level so we set it to zero
        XPGainedDuringLevel = 0;




        OnLevelUp = OnlevUp;
    }

    //Zmatias 
    //this one is supposed to return Total xp need for level
    //added (float) in calculations to avoid error from conversion to int
    public int GetXPForLevel(int level)
    {
        if (level > MAX_LEVEL)  // Todo: Throw exception dependent on game design
            return 0;

        int firstPass = 0;
        int secondPass = 0;

        for (int levelCycle = 1; levelCycle < level; levelCycle++)
        {
            firstPass += (int)Math.Floor((float)levelCycle + (300f * Math.Pow(2f, (float)levelCycle / 7f)));
            secondPass = firstPass / 4;

        }

        if (secondPass > MAX_EXP && MAX_EXP != 0)  // Todo: Throw exception dependent on game design
            return MAX_EXP;

        if (secondPass < 0)  // Todo: Throw exception dependent on game design
            return MAX_EXP;

        playerLevel = level;
        return secondPass;
    }

    //Zmatias 
    //this one is supposed to retunr level based on Total XP
    //added (float) in calculations to avoid error from conversion to int
    public int GetLevelForXP(int exp)
    {
        if (exp > MAX_EXP)
            return MAX_EXP;

        int firstPass = 0;
        int secondPass = 0;

        for (int levelCycle = 1; levelCycle <= MAX_LEVEL; levelCycle++)
        {
            firstPass += (int)Math.Floor((float)levelCycle + (300f * Math.Pow(2f, (float)levelCycle / 7f)));

            secondPass = firstPass / 4;

            if (secondPass > exp)
            {
                return levelCycle;
            }

        }

        if (exp > secondPass)
            return MAX_LEVEL;

        return 0;
    }

    public bool AddExp(int amount)
    {

        //Zmatias
        //We save xp to level XP counter no matter what
        //We save it at total as well
        XPGainedDuringLevel += amount;
        experience_Total += amount;

        //Zmatias
        //Proceed with level up. 
        //You have gained as experience as you need for the level up
        if (XPGainedDuringLevel >= XPNeed4LevelUp)
        {

            if (OnLevelUp != null)
            {
                //Zmatias
                //If more XP is gained that is needed we must transfer the rest to next level
                //Here we calculate that
                var diffXpToTransferToNextLevel = experience_Total - GetXPForLevel(currentLevel + 1);

                //Zmatias
                //Now we calculate the next level
                //We have already check , we can just add +1 to current
                //But I left it as it was
                currentLevel = GetLevelForXP(experience_Total);

                //Zmatias
                //We calculate how much Total exp we need for the next level
                //We calculate how much LevelXP we need for level up   
                nextLevel_Xp_Total = GetXPForLevel(currentLevel + 1);
                XPNeed4LevelUp = nextLevel_Xp_Total - GetXPForLevel(currentLevel);

                //Zmatias
                //transfer Xp to next level
                //Check to avoid errors during start of gameplay
                if (diffXpToTransferToNextLevel > 0)
                {
                    XPGainedDuringLevel = diffXpToTransferToNextLevel;
                }
                else
                {
                    XPGainedDuringLevel = 0;
                }

                OnLevelUp.Invoke();

            }

            return true;
        }

        //Zmatiass
        //Commented out
        /*
        if (oldLevel < GetLevelForXP(experience))
        {
            if (currentLevel < GetLevelForXP(experience))
            {
                currentLevel = GetLevelForXP(experience);

                if (OnLevelUp != null)
                {
                    //Zmatias
                    var prevLevelXpNeed=nextLevelXp;
                    Debug.Log("Prev Gained" + ExperienceGainedDuringLevel);
                    Debug.Log("Prev" + prevLevelXpNeed);

                    OnLevelUp.Invoke();

                    //Zmatias Update Variables
                    nextLevelXp =(RemainingXP(currentLevel));

                    //Zmatias
                    ExperienceGainedDuringLevel=ExperienceGainedDuringLevel-prevLevelXpNeed;
                }
                    
                return true;
            }
        }

        */

        return false;
    }


    //Zmatias
    //This is supposed to grab the difference,I think it doesnt work
    //I am not using it at all
    public int RemainingXP(int level)
    {
        if (level > MAX_LEVEL)  // Todo: Throw exception dependent on game design
            return 0;

        int firstPass = 0;
        int secondPass = 0;
        int thirdpass;
        int fourthpass = 0;

        //Zmatias added =
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            int prevLevelCycle = levelCycle - 1;

            firstPass += (int)Math.Floor((float)levelCycle + (300f * Math.Pow(2f, (float)levelCycle / 7.0)));
            secondPass += (int)Math.Floor((float)prevLevelCycle + (300f * Math.Pow(2f, (float)prevLevelCycle / 7.0)));
            thirdpass = (firstPass + secondPass);
            fourthpass = thirdpass / 4;

            //Debug.Log("Pass:Firs - > "+(int)Math.Floor((float)levelCycle));
            //Debug.Log("Pass:Firs - > "+Math.Pow(2f, (float)levelCycle / 7.0)*300);

            //Debug.Log("Pass:Second - > "+(int)Math.Floor((float)prevLevelCycle));
            //Debug.Log("Pass:Second - > "+Math.Pow(2f, (float)prevLevelCycle / 7.0)*300);

        }
        return fourthpass;
    }
}


/*

This is a C# script called XPSystem that is used to manage player experience (XP) in a game. It includes functionality for calculating and storing the amount of XP needed to reach the next level, as well as for tracking the player's current level and total XP earned throughout the game.

The script includes a few variables to store XP-related information. The XPGainedDuringLevel variable keeps track of the amount of XP the player has earned during the current level.
 The XPNeed4LevelUp variable stores the amount of XP needed to level up from the current level to the next level. The experience_Total variable stores the total amount of XP the player has earned throughout the game. 
 
 The nextLevel_Xp_Total variable stores the amount of XP needed to level up from the current level to the next level, in terms of the total amount of XP earned.

The script includes a constructor function that takes the player's starting level as a parameter and sets up the initial values for the XP-related variables. 
It also includes a couple of helper functions: GetXPForLevel calculates the amount of XP needed to reach a specific level, and GetLevelForXP calculates the player's level based on their total XP.

The AddExp function is used to add XP to the player's total. When the player earns enough XP to level up, the OnLevelUp function is called, which can be used to trigger any additional actions or updates that should happen when the player levels up.

*/