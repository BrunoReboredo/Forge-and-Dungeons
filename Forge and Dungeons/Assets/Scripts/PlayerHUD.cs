using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Slider healthBar;
    public Slider abilityBar;

    public void SetHealth(float current, float max)
    {
        healthBar.value = current / max;
    }

    public void SetAbilityCharge(float current, float max)
    {
        abilityBar.value = current / max;
    }
}
