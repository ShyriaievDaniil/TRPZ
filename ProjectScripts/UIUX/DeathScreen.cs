using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _screen;
    private void Awake()
    {
        gameObject.SetActive(false);
        EventBus.PlayerDeath += Show;
        EventBus.LevelUp += CheckBoss;
    }
    private void Show(object obj)
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
    private void CheckBoss(object obj, int level)
    {
        if (level>=5)
        {
            EventBus.EnemyDeath += Win;
        }
    }
    private void Win(object obj)
    {
        _screen.text = "You are winner!";
        Show(obj);
    }
}
