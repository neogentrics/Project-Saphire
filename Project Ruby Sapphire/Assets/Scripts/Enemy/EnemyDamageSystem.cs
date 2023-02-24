using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageSystem : MonoBehaviour
{
    /*
    public enum AttackType
    {
        Fire,
        Projectiles,
        Magic,
        Arcane,
        Water,
        Ice,
        Stone,
        Earth,
        Void,
        Light,
        Dark,
        Wind,
        Other
    }

    [System.Serializable]
    public class DamageInfo
    {
        public AttackType attackType;
        public float damageAmount;
        public float damageDuration;
        public bool isImmune;
    }

    [SerializeField] private List<DamageInfo> damageInfos = new List<DamageInfo>();

    private Enemy enemy;
    private float[] damageTimers;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        damageTimers = new float[damageInfos.Count];
    }

    public void TakeDamage(AttackType attackType, float damageAmount)
    {
        int index = GetDamageInfoIndex(attackType);
        if (index < 0 || damageInfos[index].isImmune) return;

        if (damageInfos[index].damageDuration > 0)
        {
            // Damage over time
            damageTimers[index] = damageInfos[index].damageDuration;
            StartCoroutine(DoDamageOverTime(index, damageAmount));
        }
        else
        {
            // Instant damage
            ApplyDamage(index, damageAmount);
        }
    }

    private int GetDamageInfoIndex(AttackType attackType)
    {
        for (int i = 0; i < damageInfos.Count; i++)
        {
            if (damageInfos[i].attackType == attackType)
            {
                return i;
            }
        }
        return -1;
    }

    private IEnumerator DoDamageOverTime(int index, float damageAmount)
    {
        while (damageTimers[index] > 0)
        {
            ApplyDamage(index, damageAmount * Time.deltaTime / damageInfos[index].damageDuration);
            damageTimers[index] -= Time.deltaTime;
            yield return null;
        }
    }

    private void ApplyDamage(int index, float damageAmount)
    {
        enemy.health -= damageAmount;
        enemy.health = Mathf.Clamp(enemy.health, 0f, enemy.maxHealth);

        if (enemy.health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Call loot drop function from the Enemy script
        enemy.DropLoot();
        Destroy(gameObject);
    }

    */
}


/*

Explanation:

The EnemyDamageSystem class extends MonoBehaviour, and defines an AttackType enum that lists the types of attacks that can damage the enemy, and a DamageInfo class that specifies the damage amount, duration, and immunity to each attack type.
The damageInfos field is a list of DamageInfo objects that can be set in the inspector to customize the enemy's damage vulnerability.
In Start, the script finds the Enemy component on the same game object, and initializes the damageTimers array to track the remaining duration of each damage over time effect.
The TakeDamage function is called by external scripts (e.g. the player's attack script) to apply damage to the enemy. If the attack type is not found in the damageInfos list, 
or the enemy is immune to the attack type, no damage is taken. Otherwise, if the attack causes damage over time, a coroutine is started to apply damage repeatedly over the specified duration. 
If the attack causes instant damage, the ApplyDamage function is called directly.
*/