using UnityEngine;

public enum ItemCategory
{
    Mineral,
    EnemyDrop,
    Weapon

    // Añadir más si necesario

}

public enum ItemType
{
    Coal,
    Copper,
    Iron,
    Titanium,

    // Armas
    Sword_Copper,
    Sword_Iron,
    Sword_Titanium,
    Axe_Copper,
    Axe_Iron,
    Axe_Titanium,
    Spear_Copper,
    Spear_Iron,
    Spear_Titanium,
    Hammer_Copper,
    Hammer_Iron,
    Hammer_Titanium,

    //añadir enemigos cuando se programe sus drops
}

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public string description;

    public ItemCategory category;
    public ItemType type;

    // Constructor
    public Item(ItemType itemType)
    {
        type = itemType;
        itemName = itemType.ToString();
        category = GetDefaultCategory(itemType);
    }

    private ItemCategory GetDefaultCategory(ItemType type)
    {
        switch (type)
        {
            case ItemType.Coal:
            case ItemType.Copper:
            case ItemType.Iron:
            case ItemType.Titanium:
                return ItemCategory.Mineral;


            //drops enemigos

            default:
                return ItemCategory.Mineral; // Por defecto
        }
    }
}
