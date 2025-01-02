using System.Collections.Generic;
using UnityEngine;

public class UIHealthBarManager : MonoBehaviour
{
    private Dictionary<object, HealthBar> healthBars = new Dictionary<object, HealthBar>();

    void Awake()
    {
        EventBus.Created += AddHealthBar;
        EventBus.HealthChanged += ChangeHealth;
    }
    private void ChangeHealth(object obj, float percentage)
    {
        if (healthBars.ContainsKey(obj))
        {
            healthBars[obj].SetHealth(percentage);
        }
    }

    private void AddHealthBar(object obj, string type)
    {
        if (healthBars.ContainsKey(obj))
        {
            healthBars[obj].SetHealth(1);
        }
        else
        {
            GameObject healthBarPrefab;
            HealthBar healthBar;
            if (type == "Enemy") {
                Enemy enemyObject = (Enemy)obj;
                healthBarPrefab = Resources.Load<GameObject>($"UI/{type}HealthBar");
                healthBar = Instantiate(healthBarPrefab, enemyObject.transform).GetComponent<HealthBar>();
                healthBars.Add(obj, healthBar);
            }
            else
            {
                healthBarPrefab = Resources.Load<GameObject>($"UI/{type}HealthBar");
                healthBar = Instantiate(healthBarPrefab, transform).GetComponent<HealthBar>();
                healthBars.Add(obj, healthBar);
            }
        }
    }
}
