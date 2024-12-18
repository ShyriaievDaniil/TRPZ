using System.Collections.Generic;

public class StatHandler
{
    private Dictionary<Stats, int> _stats = new Dictionary<Stats, int>();

    public Dictionary<Stats, int> GetValues() {return _stats;}
    public int GetValue(Stats stat)
    {
        if (!_stats.ContainsKey(stat) || _stats[stat]<=0) {  return 0; }
        else {return _stats[stat];}
    }

    public void ChangeValue(Stats stat, int value)
    {
        if (_stats.ContainsKey(stat)){
            _stats[stat] += value;
        } else
        {
            _stats.Add(stat, value);
        }
    }
}
