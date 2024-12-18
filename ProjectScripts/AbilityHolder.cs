

using UnityEngine;

public class AbilityHolder{
    private float _currentCooldown = 0;
    private Ability _ability;
    private Player _owner;
    public AbilityHolder(Ability ability, Player owner)
    {
        _owner = owner;
        SetAbility(ability);
    }
    public void TryActivate()
    {
        if (_owner.Mana >= _ability.Manacost && _currentCooldown == 0)
        {
            _ability.Activate(_owner);
            _currentCooldown = _ability.Cooldown;
            _owner.ConsumeMana(_ability.Manacost);
        }
    }
    public void SetAbility(Ability ability)
    {
        _ability = ability;
    }
    public void ReduceCooldown(float cooldown)
    {
        if (_currentCooldown < cooldown){
            _currentCooldown = 0;
        }
        else{
            _currentCooldown -= cooldown;
        }
    }
}
