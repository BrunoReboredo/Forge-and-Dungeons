using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public static PlayerWeaponController Instance;
    [SerializeField] private WeaponAttack weaponAttack;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EquipWeapon(WeaponStats stats)
    {
        weaponAttack.SetWeapon(stats);
    }
}
