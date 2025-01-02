using System.Collections;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] private Transform _target;
    private CharacterController _controller;
    private Coroutine _checkTargetCoroutine;
    private Coroutine _attackCoroutine;
    private EnemyWalkStrategy _walkStrategy;
    private EnemyData _data;

    public Transform _transform;
    public string type;
    public int Health => _data.currentHealth;

    public void Awake()
    {
        _transform = transform;
        _controller = GetComponent<CharacterController>();
        gameObject.SetActive(false);
    }

    public void SetData(EnemyData data)
    {
        gameObject.SetActive(true);
        _data = (EnemyData)data.Clone();
        EventBus.OnHealthChanged(this, 1);
        if (_walkStrategy == null)
        {
            _walkStrategy = new EnemyWalkStrategy(_controller, _transform, _data.speed, this);
        }
        else
        {
            _walkStrategy.SetSpeed(_data.speed);
        }
        _checkTargetCoroutine = StartCoroutine(CheckTarget());
    }

    public void SetType(string type)
    {
        this.type = type;
    }

    public void TakeDamage(int damage)
    {
        if (_data.currentHealth <= damage)
        {
            _data.currentHealth = 0;
            StopAllCoroutines();
            _target = null;
            _walkStrategy.SetTarget(null);
            gameObject.SetActive(false);
            EventBus.OnDeath(this);
        }
        else
        {
            _data.currentHealth -= damage;
        }
        EventBus.OnHealthChanged(this, (float)_data.currentHealth / _data.maxHealth);
    }

    public void Update() {
        _walkStrategy.Look();
    }

    public void FixedUpdate()
    {
        if (!_data.isAttacking) { 
            _walkStrategy.Move();
        }
    }

    private IEnumerator CheckTarget()
    {
        while (true)
        {
            if (_target)
            {
                float distance = (_target.position - _transform.position).magnitude;
                if (distance > 15)
                {
                    _target = null;
                    StopCoroutine(_attackCoroutine);
                    _walkStrategy.SetTarget(null);
                    EventBus.OnWalking(this, false);
                }
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position,
                10f, 1 << 3);
                if (colliders.Any())
                {
                    foreach (Collider collider in colliders)
                    {
                        if (collider.gameObject.GetComponent<Player>())
                        {
                            _target = collider.transform;
                            _attackCoroutine = StartCoroutine(_data.Behaviour(_target, this));
                            _walkStrategy.SetTarget(_target);
                            break;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
