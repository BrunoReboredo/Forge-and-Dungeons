using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float currentHealth;

    public float maxSpecial = 100f;
    public float currentSpecial;

    void Awake()
    {
        currentHealth = maxHealth;
        currentSpecial = 0f;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
    }

    public void AddSpecialCharge(float amount)
    {
        currentSpecial = Mathf.Clamp(currentSpecial + amount, 0f, maxSpecial);
    }
}
