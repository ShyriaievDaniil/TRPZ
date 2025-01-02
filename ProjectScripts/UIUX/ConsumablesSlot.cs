using TMPro;
using UnityEngine.EventSystems;

public class ConsumablesSlot : InventorySlot
{
    protected new Consumables _content;

    public override void InitSlot(ITooltipDescription content, Inventory inventory)
    {
        _content = (Consumables)content;
        base.InitSlot(content, inventory);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        _content.TryUse(_inventory.GetOwner());
        UpdateView();
    }
    public void UpdateView()
    {
        GetComponent<TextMeshProUGUI>().text = _content.Name;
    }
}
