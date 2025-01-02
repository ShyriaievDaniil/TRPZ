using System.Collections.Generic;
using UnityEngine;

public class StatHandler
{
    private Dictionary<Stats, int> _stats = new Dictionary<Stats, int>();
    private Player _owner;


    public StatHandler(Player owner)
    {
        _owner = owner;
        _stats.Add(Stats.MaxHP, 50);
        _stats.Add(Stats.MaxMP, 50);
        _stats.Add(Stats.ATK, 10);
        _stats.Add(Stats.DEF, 0);
    }
    public Dictionary<Stats, int> GetValues() {return _stats;}
    public int GetValue(Stats stat)
    {
        if (!_stats.ContainsKey(stat) || _stats[stat]<=0) {  return 0; }
        else {return _stats[stat];}
    }

    public void ChangeValue(Stats stat, int value)
    {
        if (_stats.ContainsKey(stat))
        {
            _stats[stat] += value;
        }
        else
        {
            _stats.Add(stat, value);
        }
        if (stat == Stats.MaxHP) {
            _owner.Notify(this, "HPChanged", value);
        }
        if (stat == Stats.MaxMP)
        {
            _owner.Notify(this, "MPChanged", value);
        }
    }
    public void ChangeValues(Dictionary<Stats, int> values)
    {
        foreach (KeyValuePair<Stats, int> stat in values)
        {
            ChangeValue(stat.Key, stat.Value);
        }
    }
}
