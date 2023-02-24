using UnityEngine;

public struct DamageSystem
{

    // Variables
    public int baseDamage;
    public int dotDamage;
    public int elementDamage;
    public int arenaDamage;
    public int totalDamage;


    public int CalculateDamage()
    {
        totalDamage = baseDamage + dotDamage 
            + elementDamage + arenaDamage;
        return totalDamage;
    }

    public void ApplyWeakness()
    {
        elementDamage *= 2;
    }

    public void ApplyPoison()
    {
        dotDamage += 5;
    }

    public void ApplyFire()
    {
        elementDamage += 10;
    }        

    void Die()
    {
        // Code for player death
        // ...
    }
}