using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Elementos UI")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Text timerText;
    [SerializeField] private Slider weaponDurability;
    [SerializeField] private GameObject inventoryPanel;

    private float maxHealth = 100f;
    private float currentHealth;
    private float matchTime = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        matchTime += Time.deltaTime;
        UpdateTimerUI();
    }

    public void UpdateHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.value = currentHealth / maxHealth;
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = FormatTime(matchTime);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
