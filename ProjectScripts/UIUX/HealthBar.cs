using UnityEngine;
using UnityEngine.UI;

public class HealthBar: MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private Image _health;

    public void SetHealth(float percentage)
    {
        _health.fillAmount = percentage;
    }
}
