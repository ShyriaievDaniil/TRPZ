using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHealth, IMana, IMediator
{
    [SerializeField] private int _currentHealth=100;
    [SerializeField] private int _currentMana=100;
    [SerializeField] private int _level = 1;
    [SerializeField] private int _experience = 0;
    [SerializeField] private int _needExperience = 50;
    [SerializeField] private Inventory _inventory;
    private StatHandler _stats;
    private CharacterController _characterController;
    private InputController _inputController;
    private AbilitySystem _abilitySystem;
    private EquipmentHolder _equipment;
    private WalkStrategy _walkStrategy;
    private WalkStrategy _fightWalkStrategy;
    private WalkStrategy _idleWalkStratedy;
    public int Health => _currentHealth;
    public int Mana => _currentMana;

    public void Awake()
    {
        _stats = new StatHandler(this);
        _currentHealth = _stats.GetValue(Stats.MaxHP);
        _currentMana = _stats.GetValue(Stats.MaxMP);

        _equipment = new EquipmentHolder(this);

        _characterController = GetComponent<CharacterController>();
        _fightWalkStrategy = new FightWalkStrategy(_characterController, transform, 8f, this);
        _idleWalkStratedy = new IdleWalkStrategy(_characterController, transform, 10f, this);
        _walkStrategy = _idleWalkStratedy;

        _inputController = new InputController(this);

        _abilitySystem = new AbilitySystem(this);
        
        EventBus.EnemyDeath += ExpUp;
        EventBus.OnCreated(this, "Player");
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
    public StatHandler GetStats()
    {
        return _stats;
    }
    public void TakeDamage(int damage)
    {
        damage -= _stats.GetValue(Stats.DEF);
        if (_currentHealth <= damage)
        {
            _currentHealth = 0;
            EventBus.OnDeath(this);
        }
        else
        {
            _currentHealth -= damage;
        }
        EventBus.OnHealthChanged(this, (float)_currentHealth / _stats.GetValue(Stats.MaxHP));
    }
    public void Heal(int healing) {
        if (_currentHealth + healing > _stats.GetValue(Stats.MaxHP)) { 
            _currentHealth = _stats.GetValue(Stats.MaxHP);
        }
        else
        {
            _currentHealth += healing;
        }
        EventBus.OnHealthChanged(this, (float)_currentHealth / _stats.GetValue(Stats.MaxHP));
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
        EventBus.OnManaChanged((float)_currentMana / _stats.GetValue(Stats.MaxMP));
    }
    public void RestoreMana(int value)
    {
        if (_currentMana + value > _stats.GetValue(Stats.MaxMP))
        {
            _currentMana = _stats.GetValue(Stats.MaxMP);
        }
        else
        {
            _currentMana += value;
        }
        EventBus.OnManaChanged((float)_currentMana / _stats.GetValue(Stats.MaxMP));
    }
    private void ExpUp(object obj) { 
        Enemy enemy = (Enemy)obj;
        switch (enemy.type)
        {
            case "Skeleton_Minion":
                _experience += 10; break;
            case "Skeleton_Warrior":
                _experience += 20; break;
            case "Skeleton_Mage":
                _experience += 30; break;
        }
        if(_experience >= _needExperience)
        {
            _experience -= 50;
            _level += 1;
            _needExperience += 20;
            EventBus.OnLevelUp(this, _level);
        }
        EventBus.OnExpChanged((float)_experience / _needExperience);
    }
    public void Notify(object sender, string action, object data) {
        if (sender == _inputController && action == "UseAbility") {
            _abilitySystem.ActivateAbility((AbilityTypes)data);
        }
        if (sender == _inputController && action == "SwitchWalkStrategy")
        {
            SwitchWalkStrategy();
        }
        if(sender == _equipment && action == "ChangeStats")
        {
            Dictionary<Stats, int> changedStats = (Dictionary<Stats, int>)data;
            _stats.ChangeValues(changedStats);
        }
        if(sender == _stats)
        {
            int value = (int)data;
            if(action == "HPChanged")
            {
                _currentHealth += value;
            }
            if(action == "MPChanged"){
                _currentMana += value;
            }
        }
        if(sender == (object)_inventory && action == "EquipEquipment")
        {
            _equipment.Equip((Equipment)data);
            UpdateInventory();
        }
        if (sender == (object)_inventory && action == "EquipAbility")
        {
            _abilitySystem.AddAbility((Ability)data);
            UpdateInventory();
        }
        if(sender == _inputController && action == "ToggleInventory")
        {
            if (_inventory.gameObject.activeSelf)
            {
                _inventory.gameObject.SetActive(false);
            }
            else {
                _inventory.Open(_stats.GetValues(), _equipment.GetEquipment());
            }
        }
        if(sender == _inputController && action == "AddEquipment")
        {
            _inventory.AddEquipment(Resources.Load<Equipment>("Equipment/SteelSword"));
            _inventory.AddEquipment(Resources.Load<Equipment>("Equipment/ArcherBoots"));
            _inventory.AddEquipment(Resources.Load<Equipment>("Equipment/WarriorHelmet"));
            _inventory.AddEquipment(Resources.Load<Equipment>("Equipment/MageRobe"));
            _inventory.AddEquipment(Resources.Load<Equipment>("Equipment/WoodenStaff"));
            _inventory.AddAbility(Resources.Load<Ability>("Ability/MightySlice"));
            _inventory.AddAbility(Resources.Load<Ability>("Ability/LongTeleport"));
            _inventory.AddAbility(Resources.Load<Ability>("Ability/ShortTeleport"));
            _inventory.AddAbility(Resources.Load<Ability>("Ability/ArrowShot"));
            _inventory.AddAbility(Resources.Load<Ability>("Ability/BombThrow"));
            _inventory.AddConsumable(new Consumables(Consumables.Types.HPPotion, 2));
            _inventory.AddConsumable(new Consumables(Consumables.Types.MPPotion, 2));
        }
    }
    private void UpdateInventory()
    {
        if (_inventory.IsOpen()) _inventory.Open(_stats.GetValues(), _equipment.GetEquipment());
    }
    private void SwitchWalkStrategy()
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
