using UnityEngine;
public abstract class Ability : ScriptableObject, ITooltipDescription
{
    [SerializeField] protected string _name;
    [SerializeField] protected int _manacost;
    [SerializeField] protected float _cooldown;
    [SerializeField] protected AbilityTypes _type;
    public string Name => _name;
    public int Manacost => _manacost;
    public float Cooldown => _cooldown;
    public AbilityTypes Type => _type;
    
    public abstract void Activate(Player owner);

    public abstract string GetDescription();
}
