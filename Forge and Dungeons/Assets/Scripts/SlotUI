using UnityEngine;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    private Inventory.InventorySlot slotData;

    public void Setup(Inventory.InventorySlot data)
    {
        slotData = data;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Mostrar menú contextual con la posición del ratón
            ContextMenuUI.Instance.Show(slotData, Input.mousePosition);
        }
    }
}