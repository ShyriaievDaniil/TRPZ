using UnityEngine;

[CreateAssetMenu(fileName = "Equipment")]
public class Equipment: ScriptableObject, ITooltipDescription
{
    [SerializeField] private string _name;
    [SerializeField] private Stats _mainStat;
    [SerializeField] private Stats _secondaryStat;
    [SerializeField] private int _mainStatValue;
    [SerializeField] private int _secondaryStatValue;
    [SerializeField] private Types _type;
    public string Name => _name;
    public Stats MainStat => _mainStat;
    public Stats SecondaryStat => _secondaryStat;
    public int MainStatValue => _mainStatValue;
    public int SecondaryStatValue => _secondaryStatValue;
    public Types Type => _type;
    public enum Types
    {
        Weapon,
        Helmet,
        Chestplate,
        Boots,
    }
    public string GetDescription()
    {
        return $"{_name}\n {_type}\n {_mainStat}:{_mainStatValue}\n {_secondaryStat}:{_secondaryStatValue}";
    }
}
