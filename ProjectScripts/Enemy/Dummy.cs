using UnityEngine;

public class Dummy : MonoBehaviour, IHealth
{
    [SerializeField] private int _health;
    public int Health => _health;

    public void TakeDamage(int damage)
    {
        if (_health <= damage)
        {
            _health = 0;
            Debug.Log("You killed dummy!");
        }
        else
        {
            _health -= damage;
        }
    }
}
