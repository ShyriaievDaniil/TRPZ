using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : HealthBar
{
    [SerializeField] private Image _mana;
    [SerializeField] private Image _exp;
    [SerializeField] private TextMeshProUGUI _level;

    private void Awake()
    {
        EventBus.ExpChanged += SetExp;
        EventBus.ManaChanged += SetMana;
        EventBus.LevelUp += SetLevel;
    }
    public void SetMana(float percentage)
    {
        _mana.fillAmount = percentage;
    }
    public void SetExp(float percentage)
    {
        _exp.fillAmount = percentage;
    }
    public void SetLevel(object obj, int level) {
        _level.text = level.ToString();
    }
}
