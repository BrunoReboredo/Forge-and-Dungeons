using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject slotInventoryPrefab;
    [SerializeField] Transform slotsParent;
    [SerializeField] Image backgroundImage; // Imagen de fondo
    [SerializeField] Vector2 cellSize = new Vector2(30, 30); // Tamaño de la celda (30x30 píxeles)

    private bool isInventoryOpen = false;

    [System.Serializable]
    public class InventorySlot
    {
        public Item item;
        public int quantity;

        public InventorySlot(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }

    public List<InventorySlot> slots = new List<InventorySlot>();
    public int maxSlots = 15;  // Modificable el número de slots
    public int maxStackSize = 25;
    public int slotsPerRow = 5;  // Cuántos slots por fila (para calcular el tamaño del fondo)

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Asegúrate de que el inventario esté cerrado al inicio
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowInventory();
        }
    }

    public bool AddItem(Item newItem)
    {
        foreach (var slot in slots)
        {
            if (slot.item.type == newItem.type && slot.quantity < maxStackSize)
            {
                slot.quantity++;
                return true;
            }
        }

        if (slots.Count >= maxSlots)
        {
            return false;
        }

        slots.Add(new InventorySlot(newItem, 1));
        return true;
    }

    public void RemoveItem(ItemType type, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item.type == type)
            {
                slots[i].quantity -= amount;

                if (slots[i].quantity <= 0)
                    slots.RemoveAt(i);

                return;
            }
        }
    }

    public void ShowInventory()
    {
        if (!isInventoryOpen)
        {
            inventoryUI.SetActive(true);
            isInventoryOpen = true;

            // Limpiar cualquier slot existente
            foreach (Transform child in slotsParent)
            {
                Destroy(child.gameObject);
            }

            // Crear los slots y ajustar el fondo
            int slotsToCreate = maxSlots;

            for (int i = 0; i < slotsToCreate; i++)
            {
                GameObject newSlot = Instantiate(slotInventoryPrefab, slotsParent);

                Image slotIcon = newSlot.GetComponentInChildren<Image>();
                Text slotQuantity = newSlot.GetComponentInChildren<Text>();

                if (i < slots.Count)
                {
                    InventorySlot currentSlot = slots[i];

                    if (slotIcon != null)
                    {
                        slotIcon.sprite = currentSlot.item.icon;
                    }

                    if (slotQuantity != null)
                    {
                        slotQuantity.text = currentSlot.quantity.ToString();
                    }
                }
                else
                {
                    if (slotIcon != null)
                    {
                        slotIcon.sprite = null;
                    }

                    if (slotQuantity != null)
                    {
                        slotQuantity.text = "";
                    }
                }
            }

            // Ajustar el fondo al tamaño de los slots
            AdjustBackgroundSize(slotsToCreate);
        }
        else
        {
            inventoryUI.SetActive(false);
            isInventoryOpen = false;
        }
    }

    private void AdjustBackgroundSize(int slotsToCreate)
    {
        // Calcular cuántas filas de slots necesitamos
        int rows = Mathf.CeilToInt((float)slotsToCreate / slotsPerRow);

        // Ajustar el tamaño del fondo de acuerdo con el número de filas y columnas
        float backgroundWidth = slotsPerRow * cellSize.x; // Número de columnas * tamaño de la celda
        float backgroundHeight = rows * cellSize.y; // Número de filas * tamaño de la celda

        // Ajustar el tamaño del fondo
        backgroundImage.rectTransform.sizeDelta = new Vector2(backgroundWidth, backgroundHeight);
    }
}