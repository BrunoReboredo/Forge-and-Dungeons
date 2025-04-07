using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Aquí puedes poner efectos de muerte más adelante
        Destroy(gameObject);
    }
}
