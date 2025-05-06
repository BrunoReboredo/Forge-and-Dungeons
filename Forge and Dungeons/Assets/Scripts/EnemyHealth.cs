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

    public void TakeDamage(float amount) // Usamos float para compatibilidad con daño de arma
    {
        if (isRegenerating) return;

        currentHealth -= Mathf.RoundToInt(amount); // Convertimos float a int
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
        yield return new WaitForSeconds(3f); // Tiempo para regenerar
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

    // Este método se usará si el objeto tiene un collider y recibe golpes
    private void OnCollisionEnter(Collision collision)
    {
        // Comprobamos si el objeto colisionado tiene una etiqueta de "Sword" o "Enemy"
        if (collision.gameObject.CompareTag("Sword"))
        {
            // Aplica daño al recibir un golpe con la espada
            TakeDamage(100); // Ajusta el valor de daño según sea necesario
        }
    }
}

