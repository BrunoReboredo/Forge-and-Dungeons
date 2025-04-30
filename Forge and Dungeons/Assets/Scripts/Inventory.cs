using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject slotInventoryPrefab;
    [SerializeField] Transform slotsParent;
    [SerializeField] Image backgroundImage;
    [SerializeField] Vector2 cellSize = new Vector2(30, 30);
    public Inventory.InventorySlot slotData; // Aquí tienes acceso a item y quantity
    public GameObject contextMenuPrefab;

    [SerializeField] MonoBehaviour playerController;

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
    public int maxSlots = 15;
    public int maxStackSize = 25;
    public int slotsPerRow = 5;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        inventoryUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
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

        if (slots.Count >= maxSlots) return false;

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

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        inventoryUI.SetActive(isInventoryOpen);

        // Activar/desactivar control del jugador
        if (playerController != null)
            playerController.enabled = !isInventoryOpen;

        // Mostrar/ocultar cursor
        Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isInventoryOpen;

        if (isInventoryOpen)
        {
            UpdateInventoryUI();
        }
    }

    private void UpdateInventoryUI()
    {
        foreach (Transform child in slotsParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxSlots; i++)
        {
            GameObject newSlot = Instantiate(slotInventoryPrefab, slotsParent);

            Image slotIcon = newSlot.transform.Find("Icon")?.GetComponent<Image>();
            Text slotQuantity = newSlot.transform.Find("Quantity")?.GetComponent<Text>();

            if (i < slots.Count)
            {
                InventorySlot currentSlot = slots[i];

                if (slotIcon != null)
                    slotIcon.sprite = currentSlot.item.icon;

                if (slotQuantity != null)
                    slotQuantity.text = currentSlot.quantity.ToString();
            }
            else
            {
                if (slotIcon != null) slotIcon.sprite = null;
                if (slotQuantity != null) slotQuantity.text = "";
            }
        }

        AdjustBackgroundSize(maxSlots);
    }

    private void AdjustBackgroundSize(int slotsToCreate)
    {
        int rows = Mathf.CeilToInt((float)slotsToCreate / slotsPerRow);
        float width = slotsPerRow * cellSize.x;
        float height = rows * cellSize.y;
        backgroundImage.rectTransform.sizeDelta = new Vector2(width, height);
    }

}