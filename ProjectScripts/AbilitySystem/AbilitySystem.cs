using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem{
    private Dictionary<AbilityTypes, AbilityHolder> _abilities = new Dictionary<AbilityTypes, AbilityHolder>();
    private Player _player;
    public AbilitySystem(Player player) {
        _player = player;
    }
    public void ReduceCooldowns()
    {
        foreach (var abilityHolder in _abilities)
        {
            abilityHolder.Value.ReduceCooldown(Time.deltaTime);
        }
    }
    public void ActivateAbility(AbilityTypes type)
    {
        if (_abilities.ContainsKey(type)){
            _abilities[type].TryActivate();
        }
    }
    public void AddAbility(Ability ability)
    {
        if (_abilities.ContainsKey(ability.Type))
        {
            _abilities[ability.Type].SetAbility(ability);
        }
        else {
            _abilities.Add(ability.Type, new AbilityHolder(ability, _player));
        }
    }
}
