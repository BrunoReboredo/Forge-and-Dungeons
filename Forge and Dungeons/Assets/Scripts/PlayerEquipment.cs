using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public Transform weaponHolder; // Lugar donde se instancian las armas
    private GameObject currentWeapon;
    private WeaponAttack weaponAttack; // Referencia a WeaponAttack para actualizar stats

    void Start()
    {
        // Obtener referencia a WeaponAttack
        weaponAttack = GetComponent<WeaponAttack>();
    }

    public void EquipNewWeapon(GameObject newWeaponPrefab)
    {
        // Destruir el arma anterior si existiese
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // Instanciar nueva arma en la "mano" del jugador
        currentWeapon = Instantiate(newWeaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        currentWeapon.transform.localScale = Vector3.one * 25f;

        // Obtener el componente WeaponInstance de la nueva arma
        WeaponInstance weaponInstance = currentWeapon.GetComponent<WeaponInstance>();

        // Verificar si tiene WeaponInstance y stats
        if (weaponInstance != null && weaponInstance.stats != null)
        {
            // Asignar los stats de la nueva arma al WeaponAttack
            weaponAttack.stats = weaponInstance.stats;

            // Equipar el modelo del arma
            weaponAttack.EquipWeaponModel();
        }
    }
}

