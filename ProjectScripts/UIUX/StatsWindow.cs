using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _window;

    private void Awake()
    {
        _window = GetComponent<TextMeshProUGUI>();
    }
    public void Draw(Dictionary<Stats, int> stats)
    {
        string info = "Stats: \n";
        foreach(KeyValuePair<Stats, int> stat in stats)
        {
            info += $"{stat.Key}:{stat.Value}\n";
        }
        _window.text = info;
    }
}
