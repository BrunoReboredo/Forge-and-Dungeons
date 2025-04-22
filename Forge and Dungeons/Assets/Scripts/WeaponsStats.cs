using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Weapons/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    public WeaponType weaponType;
    public MaterialType materialType;

    public float baseDamage;
    public float range;
    public float attackRate; // ataques por segundo
    public float durability;

    public float GetDamage()
    {
        float materialMultiplier = materialType switch
        {
            MaterialType.Copper => 1f,
            MaterialType.Iron => 1.5f,
            MaterialType.Titanium => 2f,
            _ => 1f
        };

        return baseDamage * materialMultiplier;
    }

    public float GetDurability()
    {
        return materialType switch
        {
            MaterialType.Copper => 100f,
            MaterialType.Iron => 200f,
            MaterialType.Titanium => 400f,
            _ => 100f
        };
    }
}