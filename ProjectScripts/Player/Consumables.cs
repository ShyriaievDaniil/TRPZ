public class Consumables: ITooltipDescription
{
    private Types _type;
    private int _quantity;

    public string Name => $"{_type.ToString()} X{_quantity}";
    public enum Types
    {
        HPPotion,
        MPPotion,
    }

    public Consumables(Types type, int quantity)
    {
        _type = type;
        _quantity = quantity;
    }

    public void TryUse(Player target)
    {
        if (_quantity > 0) {
            if (_type == Types.HPPotion)
            {
                target.Heal(10);
            }
            if (_type == Types.MPPotion)
            {
                target.RestoreMana(10);
            }
            _quantity--;
        }
    }
    public void AddQuantity(int value)
    {
        _quantity += value;
    }
    public string GetDescription()
    {
        if(_type == Types.HPPotion)
        {
            return "Restore 10 HP";
        }
        if(_type == Types.MPPotion)
        {
            return "Restore 10 MP";
        }
        else
        {
            return "No Description";
        }
    }
    public int GetQuantity() { return _quantity; }
    public new Types GetType() { return _type; }
}
