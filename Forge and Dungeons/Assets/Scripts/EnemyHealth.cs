using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
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

    public void TakeDamage(float amount)
    {
        if (isRegenerating) return;

        currentHealth -= Mathf.RoundToInt(amount);
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthBar();

        if (currentHealth == 0)
        {
            StartCoroutine(Regenerate());
        }
    }

    private IEnumerator Regenerate()
    {
        isRegenerating = true;
        yield return new WaitForSeconds(3f); // Tiempo para regenerar
        currentHealth = maxHealth;
        isRegenerating = false;
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
            // no me acuerdo por q esta pero como funciona bien con los daños de cada arma no tocar
            TakeDamage(100);
        }
    }
}

