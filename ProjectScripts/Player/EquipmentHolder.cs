using System;
using System.Collections.Generic;

public class EquipmentHolder
{
    private Dictionary<Equipment.Types, Equipment> _equipment;
    private Player _owner;

    public EquipmentHolder(Player owner) { 
        _equipment = new Dictionary<Equipment.Types, Equipment>();
        _owner = owner;
    }

    public void Equip(Equipment equipment)
    {
        Dictionary<Stats, int> changedStats = new Dictionary<Stats, int>();
        foreach (Stats stat in Enum.GetValues(typeof(Stats)))
        {
            changedStats.Add(stat, 0);
        }
        if (_equipment.ContainsKey(equipment.Type))
        {
            Equipment old = _equipment[equipment.Type];
            changedStats[equipment.MainStat] += equipment.MainStatValue;
            changedStats[equipment.SecondaryStat] += equipment.SecondaryStatValue;
            changedStats[old.MainStat] -= old.MainStatValue;
            changedStats[old.SecondaryStat] -= old.SecondaryStatValue;
            _equipment[equipment.Type] = equipment;
            _owner.Notify(this, "ChangeStats", changedStats);
        }
        else
        {
            _equipment.Add(equipment.Type, equipment);
            changedStats[equipment.MainStat] += equipment.MainStatValue;
            changedStats[equipment.SecondaryStat] += equipment.SecondaryStatValue;
            _owner.Notify(this, "ChangeStats", changedStats);
        }
    }
    public Dictionary<Equipment.Types, Equipment> GetEquipment() {  return _equipment; }
}
