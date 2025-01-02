using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    protected ITooltipDescription _content;
    protected Inventory _inventory;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _inventory.ShowTooltip(_content.GetDescription(), eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inventory.HideTooltip();
    }
    public ITooltipDescription GetContent()
    {
        return _content;
    }
    public virtual void InitSlot(ITooltipDescription content, Inventory inventory) { 
        _inventory = inventory;
        _content = content;
        GetComponent<TextMeshProUGUI>().text = content.Name;
    }
    public abstract void OnPointerClick(PointerEventData eventData);
}
