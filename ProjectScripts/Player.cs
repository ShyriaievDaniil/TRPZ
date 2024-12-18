using UnityEngine;

public class Player : MonoBehaviour, IHealth, IMana, IMediator
{
    [SerializeField] private int _currentHealth=100;
    [SerializeField] private int _currentMana=100;
    private StatHandler _stats;
    private CharacterController _characterController;
    private InputController _inputController;
    private AbilitySystem _abilitySystem;
    private WalkStrategy _walkStrategy;
    private WalkStrategy _fightWalkStrategy;
    private WalkStrategy _idleWalkStratedy;
    public int Health => _currentHealth;
    public int Mana => _currentMana;

    public void Awake()
    {
        _stats = new StatHandler();
        _characterController = GetComponent<CharacterController>();
        _fightWalkStrategy = new FightWalkStrategy(_characterController, transform, 8f, this);
        _idleWalkStratedy = new IdleWalkStrategy(_characterController, transform, 10f, this);
        _walkStrategy = _idleWalkStratedy;
        _inputController = new InputController(this);
        _abilitySystem = new AbilitySystem(this);
        Ability ability = Resources.Load<Ability>("Ability/SwordSlash");
        _abilitySystem.AddAbility(ability);
    }
    public void Update()
    {
        _inputController.CheckInput();
        _walkStrategy.Look();
        _abilitySystem.ReduceCooldowns();
    }
    public void FixedUpdate()
    {
        _walkStrategy.Move();
    }
    public void TakeDamage(int damage)
    {
        if (_currentHealth <= damage)
        {
            _currentHealth = 0;
        }
        else
        {
            _currentHealth -= damage;
        }
    }
    public void ConsumeMana(int value)
    {
        if (_currentMana < value)
        {
            _currentMana = 0;
        }
        else
        {
            _currentMana -= value;
        }
    }
    public void Notify(object sender, string action, object data) {
        if (sender == (object)_inputController && action == "UseAbility") {
            _abilitySystem.ActivateAbility((AbilityTypes)data);
        }
    }
    public void SwitchWalkStrategy()
    {
        if (_walkStrategy == _idleWalkStratedy)
        {
            _walkStrategy = _fightWalkStrategy;
        }
        else
        {
            _walkStrategy = _idleWalkStratedy;
        }
    }
}
