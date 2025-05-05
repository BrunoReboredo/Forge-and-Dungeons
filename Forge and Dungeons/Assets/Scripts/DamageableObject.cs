using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageableObject : MonoBehaviour
{
    public int maxHealth = 1000;
    private int currentHealth;
    private bool isRegenerating = false;

    [Header("UI")]
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        if (isRegenerating) return;

        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log($"Objeto dañado. Vida restante: {currentHealth}");
        UpdateHealthBar();

        if (currentHealth == 0)
        {
            Debug.Log("Objeto destruido. Comenzando regeneración...");
            StartCoroutine(Regenerate());
        }
    }

    private IEnumerator Regenerate()
    {
        isRegenerating = true;
        yield return new WaitForSeconds(3f);
        currentHealth = maxHealth;
        isRegenerating = false;
        Debug.Log("Objeto regenerado.");
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            TakeDamage(100);
        }
    }
}