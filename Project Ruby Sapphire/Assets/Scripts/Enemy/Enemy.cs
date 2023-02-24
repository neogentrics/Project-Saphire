using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance { get; private set; }

    [SerializeField] private int health = 100;  // starting health
    [SerializeField] private int maxHealth = 100;  // maximum health
    [SerializeField] private string enemyType = "Normal"; // type of enemy

    private bool isDead = false;

    // Call this function when the enemy takes damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Call this function when the enemy dies
    private void Die()
    {
        if (isDead) return; // only drop loot once
        isDead = true;
        LootDrop();
        Destroy(gameObject); // remove enemy from the scene
    }

    // Call this function to drop loot
    private void LootDrop()
    {
        // TODO: implement loot drop logic
        Debug.Log("Enemy dropped loot!");
    }

    // Define a custom inspector interface for the serialized fields
    [System.Serializable]
    public class EnemyStats
    {
        public int health;
        public int maxHealth;
        public string enemyType;
    }

    // Return the enemy stats as an EnemyStats object for serialization
    public EnemyStats GetEnemyStats()
    {
        EnemyStats stats = new EnemyStats();
        stats.health = health;
        stats.maxHealth = maxHealth;
        stats.enemyType = enemyType;
        return stats;
    }

    // Set the enemy stats from an EnemyStats object after deserialization
    public void SetEnemyStats(EnemyStats stats)
    {
        health = stats.health;
        maxHealth = stats.maxHealth;
        enemyType = stats.enemyType;
    }
}


/*

Explanation:

The script defines a Enemy class that extends the MonoBehaviour class, which is the base class for all Unity scripts.
The health and maxHealth fields are marked with the [SerializeField] attribute to make them visible in the Unity inspector and allow them to be serialized.
The enemyType field is also marked with [SerializeField], and is a string that can be set to describe the type of enemy.
The TakeDamage function is called when the enemy takes damage, and reduces the health field by the given amount. If the health reaches zero or less, the Die function is called.
The Die function sets the isDead flag to true, calls LootDrop, and destroys the game object.
The LootDrop function currently just logs a message to the console, but it can be expanded to drop actual loot based on some logic.
The EnemyStats class is defined as a nested class with the [System.Serializable] attribute to allow it to be serialized by Unity.
The GetEnemyStats function returns an EnemyStats object with the current values of the health, maxHealth, and enemyType fields.
The SetEnemyStats function takes an EnemyStats object and sets the health, maxHealth, and enemyType fields to its values after deserialization.

*/