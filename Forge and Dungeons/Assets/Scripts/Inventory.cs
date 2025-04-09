using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    // Para abrir el inventario
    [SerializeField] GameObject inventoryUI;

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
    public int maxSlots = 20;
    public int maxStackSize = 25;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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
        // Buscar si ya hay un slot con el mismo tipo de ítem y con espacio
        foreach (var slot in slots)
        {
            if (slot.item.type == newItem.type && slot.quantity < maxStackSize)
            {
                slot.quantity++;
                Debug.Log("Añadido al stack existente: " + newItem.itemName + " (x" + slot.quantity + ")");
                return true;
            }
        }

        // Si no hay stack disponible, crear uno nuevo si hay espacio
        if (slots.Count >= maxSlots)
        {
            Debug.Log("Inventario lleno");
            return false;
        }

        slots.Add(new InventorySlot(newItem, 1));
        Debug.Log("Objeto añadido en nuevo slot: " + newItem.itemName);
        return true;
    }

    public void RemoveItem(ItemType type, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item.type == type)
            {
                slots[i].quantity -= amount;
                Debug.Log($"Quitado {amount} de {type} - Restante: {slots[i].quantity}");

                if (slots[i].quantity <= 0)
                    slots.RemoveAt(i);

                return;
            }
        }

        Debug.LogWarning("No se encontró el ítem para remover: " + type);
    }

    public void ShowInventory()
    {
        if (slots.Count == 0)
        {
            Debug.Log("Inventario vacío");
            return;
        }

        Debug.Log("=== Inventario ===");
        foreach (var slot in slots)
        {
            Debug.Log($"- {slot.item.itemName} x{slot.quantity} [{slot.item.category}]");
        }
    }
}


