using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    /*
    public enum AttackType
    {
        CloseRange,
        MidRange,
        LongRange
    }

    [SerializeField] private AttackType attackType = AttackType.CloseRange;
    [SerializeField] private float attackRange = 20f;
    [SerializeField] private float attackCooldown = 1f;

    private Enemy enemy;
    private GameObject player;
    private float lastAttackTime = 0f;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null || enemy == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // TODO: Implement attack logic based on the attack type
        Debug.Log($"{enemy.enemyType} attacked player with {attackType} attack!");

        lastAttackTime = Time.time;
    }

    */
}

/*

Explanation:

The EnemyAttack class extends MonoBehaviour, and defines an AttackType enum that can be set in the inspector to determine the type of attack based on the enemy type.
The attackRange field determines the maximum distance at which the enemy can attack the player.
The attackCooldown field determines the time in seconds between consecutive attacks.
In Start, the script finds the Enemy component on the same game object, and finds the player game object by searching for the tag "Player".
In Update, the script checks if the player is within attackRange of the enemy, and if so, calls the Attack function.
In Attack, the script can implement custom attack logic based on the attackType and the enemy's enemyType. The example implementation just logs a message to the console, but this can be expanded to apply damage to the player, play animations, etc.
The lastAttackTime field is used to track the time of the last attack, and ensure that the enemy doesn't attack too frequently.

*/