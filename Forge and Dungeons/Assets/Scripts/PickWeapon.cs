using UnityEngine;
using TMPro;
using System.Collections;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] Transform weaponHolder;
    private bool playerInRange = false;

    private GameObject PickWeapon; // Referencia al mensaje de texto

    private void Start()
    {
        // Busca el objeto de texto por nombre o etiqueta
        PickWeapon = GameObject.Find("PickWeapon");
        if (PickWeapon != null)
        {
            PickWeapon.SetActive(false); // Por si acaso
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (PickWeapon != null)
            {
                PickWeapon.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (PickWeapon != null)
            {
                PickWeapon.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Q))
        {
            EquipWeapon();
        }
    }

    private void EquipWeapon()
    {
        PlayerEquipment playerEquipment = Object.FindFirstObjectByType<PlayerEquipment>();
        if (playerEquipment != null)
        {
            playerEquipment.EquipNewWeapon(weaponPrefab);
            if (PickWeapon != null)
            {
                PickWeapon.SetActive(false);
            }

            // Llamar a la corutina para hacer el objeto invisible y luego visible
            StartCoroutine(MakeWeaponInvisible());
        }
    }

    private IEnumerator MakeWeaponInvisible()
    {
        // Haz el objeto invisible
        gameObject.SetActive(false);

        // Espera un tiempo determinado
        yield return new WaitForSeconds(5f);

        // Vuelve a hacerlo visible
        gameObject.SetActive(true);
    }

}