using UnityEngine;
using UnityEngine.EventSystems;

public class AbilitySlot : InventorySlot
{
    protected new Ability _content;
    public override void InitSlot(ITooltipDescription content, Inventory inventory)
    {
        _content = (Ability)content;
        base.InitSlot(content, inventory);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        _inventory.EquipAbility(this);
    }
}
