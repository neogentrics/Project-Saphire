using UnityEngine;

public class PlayerClassUnlockScript : MonoBehaviour
{
    public PlayerClass playerClass;
    public PlayerClassXPSystem playerClassXPSystem;

    /*
    void Start()
    {
        // Replace starting classes with new classes at level 30
        if (playerClass.level >= 30)
        {
            switch (playerClass.currentClass)
            {
                case PlayerClass.ClassType.Swordsman:
                    playerClass.currentClass = PlayerClass.ClassType.Warrior;
                    break;
                case PlayerClass.ClassType.Archer:
                    playerClass.currentClass = PlayerClass.ClassType.Bard;
                    break;
                case PlayerClass.ClassType.Enchanter:
                    playerClass.currentClass = PlayerClass.ClassType.Mage;
                    break;
                case PlayerClass.ClassType.Marauder:
                    playerClass.currentClass = PlayerClass.ClassType.Gladiator;
                    break;
            }

            // Set all locked classes to level 30
            playerClass.SetLockedClassLevel(PlayerClass.ClassType.Warrior, 30);
            playerClass.SetLockedClassLevel(PlayerClass.ClassType.Gladiator, 30);
            playerClass.SetLockedClassLevel(PlayerClass.ClassType.Mage, 30);
            playerClass.SetLockedClassLevel(PlayerClass.ClassType.Bard, 30);
            playerClass.SetLockedClassLevel(PlayerClass.ClassType.Monk, 30);
        }

        // Unlock advanced classes at level 125
        if (playerClass.level >= 125)
        {
            // MageKnight
            if (playerClass.GetClassLevel(PlayerClass.ClassType.Warrior) >= 125 &&
                playerClass.GetClassLevel(PlayerClass.ClassType.Mage) >= 75)
            {
                playerClass.UnlockAdvancedClass(PlayerClass.ClassType.MageKnight);
            }

            // MysticMonk
            if (playerClass.GetClassLevel(PlayerClass.ClassType.Monk) >= 125 &&
                playerClass.GetClassLevel(PlayerClass.ClassType.Mage) >= 75)
            {
                playerClass.UnlockAdvancedClass(PlayerClass.ClassType.MysticMonk);
            }

            // ArcaneArcher
            if (playerClass.GetClassLevel(PlayerClass.ClassType.Bard) >= 125 &&
                playerClass.GetClassLevel(PlayerClass.ClassType.Mage) >= 75)
            {
                playerClass.UnlockAdvancedClass(PlayerClass.ClassType.ArcaneArcher);
            }

            // DualWielder
            if (playerClass.GetAdvancedClassLevel(PlayerClass.AdvancedClassType.MageKnight) >= 150 &&
                playerClass.GetAdvancedClassLevel(PlayerClass.AdvancedClassType.DualWielder) >= 150)
            {
                playerClass.UnlockAdvancedClass(PlayerClass.AdvancedClassType.DualWielder);
            }

            // LightMage
            if (playerClass.GetClassLevel(PlayerClass.ClassType.Mage) >= 125)
            {
                playerClass.UnlockAdvancedClass(PlayerClass.AdvancedClassType.LightMage);
            }

            // DarkKnight and DarkMage
            if (playerClass.GetAdvancedClassLevel(PlayerClass.AdvancedClassType.LightMage) >= 150 &&
                playerClass.GetAdvancedClassLevel(PlayerClass.AdvancedClassType.DualWielder) >= 150)
            {
                playerClass.UnlockAdvancedClass(PlayerClass.AdvancedClassType.DarkKnight);
                playerClass.UnlockAdvancedClass(PlayerClass.AdvancedClassType.DarkMage);
            }
        }
    }
    */
}
