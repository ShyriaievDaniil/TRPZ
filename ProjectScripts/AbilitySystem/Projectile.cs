using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private Player _owner;
    private float _lifetime=1.5f;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * _speed;
        _lifetime -= Time.fixedDeltaTime;
        if(_lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _owner.gameObject)
        {
            other.GetComponent<IHealth>()?.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
    public void Init(float speed, int damage, Player owner)
    {
        _speed = speed;
        _damage = damage;
        _owner = owner;
        gameObject.SetActive(true);
    }
}
