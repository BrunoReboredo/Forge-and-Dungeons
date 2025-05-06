using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("Sliders")]
    public Slider healthSlider;
    // public Slider specialSlider;

    [Header("Player Stats")]
    public PlayerStats playerStats;

    void Update()
    {
        if (playerStats != null)
        {
            healthSlider.value = playerStats.currentHealth / playerStats.maxHealth;
            // specialSlider.value = playerStats.currentSpecial / playerStats.maxSpecial;
        }
    }
}
