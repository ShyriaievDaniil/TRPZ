using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _window;
    [SerializeField] public GameObject background;
    private void Awake()
    {
        _window = GetComponent<TextMeshProUGUI>();
        background.SetActive(false);
    }
    public void Draw(string message, Vector2 position)
    {
        _window.text = message;
        background.transform.position = position;
    }
}
