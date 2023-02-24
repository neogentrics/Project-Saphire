using UnityEngine;

[CreateAssetMenu(menuName = "Damage/DamageType")]
public class DamageType: ScriptableObject
{

    public GameObject damageEffect;

    public void SpawnPrefabEffect(Vector3 spawnPosition)
    {
        GameObject clone = Instantiate(damageEffect, spawnPosition, Quaternion.identity) as GameObject;
    }
}


/*
 * Here is the video that references this Script
 * 
 * https://youtu.be/_q21rEaSlAs
 * 
 * Trying to see if I should use this or this 
 * 
 * https://youtu.be/4A_i8P4AMhI
 * 
 */
