using UnityEngine;
using TMPro;

public class PickupWeapon : MonoBehaviour
{
    public GameObject weaponPrefab;
    private bool playerInRange = false;

    private GameObject equipHintUI; // Referencia al mensaje de texto

    private void Start()
    {
        // Busca el objeto de texto por nombre o etiqueta
        equipHintUI = GameObject.Find("EquipHintText");
        if (equipHintUI != null)
        {
            equipHintUI.SetActive(false); // Por si acaso
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (equipHintUI != null)
            {
                equipHintUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (equipHintUI != null)
            {
                equipHintUI.SetActive(false);
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
            if (equipHintUI != null)
            {
                equipHintUI.SetActive(false); // Ocultar el mensaje
            }
            Destroy(gameObject);
        }
    }
}
