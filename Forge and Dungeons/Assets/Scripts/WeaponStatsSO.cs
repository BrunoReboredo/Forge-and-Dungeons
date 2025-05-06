using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Weapons/WeaponStats")]
public class WeaponStatsSO : ScriptableObject
{
    public WeaponType weaponType;
    public MaterialType materialType;

    public float baseDamage;
    public float range;
    public float attackRate;
    public float durability;
}