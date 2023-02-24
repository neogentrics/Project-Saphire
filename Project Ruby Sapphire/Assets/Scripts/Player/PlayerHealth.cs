using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Protected Fields
    protected BoxCollider2D boxCollider;

    // Public Fields
    public int hitpoints = 10;
    public int maxHitpoint = 10;
    public bool isAlive = true;

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    // Player can Recieve Damage/Die
    protected virtual void ReceiveDamage(DamageSystem dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoints -= dmg.baseDamage;

            GameManager.Instance.ShowText(dmg.baseDamage.ToString(), 25, Color.red, transform.position, Vector3.up * 50, 0.5f);

            if (hitpoints <= 0)
            {
                hitpoints = 0;
                Death();
            }
        }
    }

    protected void Heal(int healingAmount)
    {
        if (hitpoints == maxHitpoint)
            return;

        hitpoints += healingAmount;
        if (hitpoints > maxHitpoint)
            hitpoints = maxHitpoint;

        GameManager.Instance.ShowText("+" + healingAmount.ToString() + "HP", 25, Color.red, transform.position, Vector3.up * 30, 1.0f);
        GameManager.Instance.OnHitPointChange();
    }

    public void Respawn()
    {
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
    }
    protected virtual void Death()
    {
        isAlive = false;
        Time.timeScale = 0f;
        GameManager.Instance.deathMenu.SetActive(true);
    }
}
