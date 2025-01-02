using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _window;
    private void Awake()
    {
        _window = GetComponent<TextMeshProUGUI>();
    }
    public void Draw(Dictionary<Equipment.Types, Equipment> equipmentSet)
    {
        string info = "Current Equipment: \n";
        foreach (KeyValuePair<Equipment.Types, Equipment> equipment in equipmentSet)
        {
            info += $"{equipment.Key}:{equipment.Value.Name}\n";
        }
        _window.text = info;
    }
}
