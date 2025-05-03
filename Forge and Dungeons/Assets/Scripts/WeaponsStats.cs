using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    public WeaponType weaponType;
    public MaterialType materialType;

    public float baseDamage;
    public float range;
    public float attackRate;
    public float durability;

    public void InitializeStats()
    {
        // Valores base por tipo de arma
        switch (weaponType)
        {
            case WeaponType.Sword:
                baseDamage = 10f;
                range = 1.5f;
                attackRate = 1.2f;
                break;
            case WeaponType.Spear:
                baseDamage = 8f;
                range = 2.5f;
                attackRate = 1.0f;
                break;
            case WeaponType.Axe:
                baseDamage = 12f;
                range = 1.2f;
                attackRate = 0.9f;
                break;
            case WeaponType.Hammer:
                baseDamage = 15f;
                range = 1.0f;
                attackRate = 0.7f;
                break;
        }

        // Modificadores por material
        switch (materialType)
        {
            case MaterialType.Copper:
                baseDamage *= 0.8f;
                durability = 50f;
                break;
            case MaterialType.Iron:
                baseDamage *= 1.0f;
                durability = 100f;
                break;
            case MaterialType.Titanium:
                baseDamage *= 1.3f;
                durability = 150f;
                break;
        }
    }

    public float GetDamage()
    {
        return baseDamage;
    }
}
