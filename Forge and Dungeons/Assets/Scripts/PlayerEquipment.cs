using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public Transform weaponHolder; // Lugar donde se instancian las armas (ej: mano del jugador)
    private GameObject currentWeapon;

    public void EquipNewWeapon(GameObject newWeaponPrefab)
    {
        // Destruir el arma anterior si existe
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // Instanciar nueva arma en la mano del jugador
        currentWeapon = Instantiate(newWeaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        currentWeapon.transform.localScale = Vector3.one * 25f;
    }
}
