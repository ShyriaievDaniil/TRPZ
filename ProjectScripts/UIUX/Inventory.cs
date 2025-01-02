using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    [SerializeField] private Player _owner;
    [SerializeField] private StatsWindow _stats;
    [SerializeField] private BuildWindow _build;
    [SerializeField] private Tooltip _tooltip;
    [SerializeField] private Transform _equipmentWindow;
    [SerializeField] private Transform _abilitiesWindow;
    [SerializeField] private Transform _consumablesWindow;
    private List<EquipmentSlot> _equipment = new List<EquipmentSlot>();
    private List<AbilitySlot> _abilities = new List<AbilitySlot>();
    private List<ConsumablesSlot> _consumables = new List<ConsumablesSlot>();
    private GameObject _equipmentSlot;
    private GameObject _abilitiesSlot;
    private GameObject _consumableSlot;
    private void Awake()
    {
        gameObject.SetActive(false);
        _equipmentSlot = Resources.Load<GameObject>("UI/EquipmentSlot");
        _abilitiesSlot = Resources.Load<GameObject>("UI/AbilitiesSlot");
        _consumableSlot = Resources.Load<GameObject>("UI/ConsumableSlot");
        Ability startAbility = Resources.Load<Ability>("Ability/SwordSlash");
        AddAbility(startAbility);
        EquipAbility(_abilities[0]);
        AddConsumable(new Consumables(Consumables.Types.HPPotion, 3));
        AddConsumable(new Consumables(Consumables.Types.MPPotion, 5));
    }
    public void Open(Dictionary<Stats, int> stats, Dictionary<Equipment.Types, Equipment> equipment)
    {
        gameObject.SetActive(true);
        _stats.Draw(stats);
        _build.Draw(equipment);
    }
    public bool IsOpen()
    {
        return gameObject.activeSelf;
    }
    public void AddEquipment(Equipment equipment){
        foreach (EquipmentSlot existingSlot in _equipment) {
            if ((Equipment)existingSlot.GetContent() == equipment) return;
        }
        EquipmentSlot slot = Instantiate(_equipmentSlot, _equipmentWindow).GetComponent<EquipmentSlot>();
        slot.InitSlot(equipment, this);
        _equipment.Add(slot);
    }
    public void AddAbility(Ability ability)
    {
        foreach (AbilitySlot existingSlot in _abilities)
        {
            if ((Ability)existingSlot.GetContent() == ability) return;
        }
        AbilitySlot slot = Instantiate(_abilitiesSlot, _abilitiesWindow).GetComponent<AbilitySlot>();
        slot.InitSlot(ability, this);
        _abilities.Add(slot);
    }
    public void AddConsumable(Consumables consumable)
    {
        foreach (ConsumablesSlot existingSlot in _consumables)
        {
            Consumables existingConsumable = (Consumables)existingSlot.GetContent();
            if (existingConsumable.GetType() == consumable.GetType())
            {
                existingConsumable.AddQuantity(consumable.GetQuantity());
                existingSlot.UpdateView();
                return;
            }
        }
        ConsumablesSlot slot = Instantiate(_consumableSlot, _consumablesWindow).GetComponent<ConsumablesSlot>();
        slot.InitSlot(consumable, this);
        _consumables.Add(slot);
    }
    public void ShowTooltip(string message, Vector2 position)
    {
        _tooltip.Draw(message, position);
        _tooltip.background.SetActive(true); 
    }
    public void HideTooltip()
    {
        _tooltip.background.SetActive(false);
    }
    public Player GetOwner()
    {
        return _owner; 
    }
    public void EquipEquipment(EquipmentSlot slot)
    {
        if (_equipment.Contains(slot)) {
            _owner.Notify(this, "EquipEquipment", slot.GetContent());
        }
    }
    public void EquipAbility(AbilitySlot slot)
    {
        if (_abilities.Contains(slot))
        {
            _owner.Notify(this, "EquipAbility", slot.GetContent());
        }
    }
}
