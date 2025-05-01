using UnityEngine;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    public static ContextMenu Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Hide(); // ocultar por defecto
    }

    // Este método muestra el menú contextual en una posición específica y genera botones según el tipo de ítem seleccionado
    /*    public void Show(Inventory.InventorySlot slot, Vector3 position)
        {
            gameObject.SetActive(true);
            transform.position = position;

            // Limpiar botones anteriores
            foreach (Transform child in buttonContainer)
                Destroy(child.gameObject);

            // Crear botones estándar
            AddButton("Dividir", () =>
            {
                if (slot.quantity > 1)
                {
                    Inventory.Instance.AddItem(new Item(slot.item.type));
                    slot.quantity--;
                }
                Hide();
            });

            AddButton("Eliminar", () =>
            {
                Inventory.Instance.RemoveItem(slot.item.type, slot.quantity);
                Hide();
            });

            AddButton("Soltar", () =>
            {
                Inventory.Instance.RemoveItem(slot.item.type, 1);
                Hide();
            });

            // Si es arma, añadir "Equipar"
            if (slot.item.category == ItemCategory.Weapon)
            {
                AddButton("Equipar", () =>
                {
                    EquipWeaponFromItem(slot.item);
                    Hide();
                });
            }
        }*/

    private void AddButton(string text, UnityEngine.Events.UnityAction action)
    {
        GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
        newButton.GetComponentInChildren<Text>().text = text;
        newButton.GetComponent<Button>().onClick.AddListener(action);
    }

    // Este método convierte un Item del inventario en un WeaponStats, y lo equipa
    /*  [SerializeField] private List<WeaponStats> allWeapons;

      private void EquipWeaponFromItem(Item item)
      {
          WeaponStats matchingStats = allWeapons.Find(w =>
              w.weaponType.ToString() + "_" + w.materialType.ToString() == item.type.ToString());

          if (matchingStats != null)
          {
              PlayerWeaponController.Instance.EquipWeapon(matchingStats);
              Debug.Log($"Equipada: {matchingStats.weaponType} de {matchingStats.materialType}");
          }
          else
          {
              Debug.LogWarning("No se encontró un WeaponStats correspondiente al item.");
          }

          Hide();
      }*/


    // Este método analiza el tipo del ítem (ItemType) y lo divide por _ para extraer: El tipo de arma (Sword, Axe, etc.). El tipo de material (Iron, Titanium, etc.).
    /*  private bool ParseWeaponType(ItemType type, out WeaponType weapon, out MaterialType material)
      {
          string[] parts = type.ToString().Split('_');
          return parts.Length == 2 &&
                 System.Enum.TryParse(parts[0], out weapon) &&
                 System.Enum.TryParse(parts[1], out material);
      }*/

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
