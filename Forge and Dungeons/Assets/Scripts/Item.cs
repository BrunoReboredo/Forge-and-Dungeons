using UnityEngine;

public enum ItemCategory
{
    Mineral,
    EnemyDrop,

    // Añadir más si necesario

}

public enum ItemType
{
    Coal,
    Copper,
    Iron,
    Titanium,

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
                return ItemCategory.Mineral;
        }
    }
}
