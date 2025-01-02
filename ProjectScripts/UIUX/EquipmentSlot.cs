using UnityEngine.EventSystems;

public class EquipmentSlot : InventorySlot
{
    protected new Equipment _content;

    public override void InitSlot(ITooltipDescription content, Inventory inventory)
    {
        _content = (Equipment)content;
        base.InitSlot(content, inventory);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        _inventory.EquipEquipment(this);
    }
}
