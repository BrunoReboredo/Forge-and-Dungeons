using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; // Singleton para acceso global

    public List<Item> items = new List<Item>();
    public int maxSlots = 20;

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
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventario lleno");
            return false;
        }

        items.Add(newItem);
        Debug.Log("Objeto añadido al inventario: " + newItem.itemName);
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        Debug.Log("Objeto eliminado del inventario: " + item.itemName);
    }

    public void ShowInventory()
    {
        if (items.Count == 0)
        {
            Debug.Log("Inventario vacío");
            return;
        }

        Debug.Log("=== Inventario ===");
        foreach (var item in items)
        {
            Debug.Log($"- {item.itemName} [{item.category}]");
        }
    }
}

