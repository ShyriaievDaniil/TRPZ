using UnityEngine;

[CreateAssetMenu(fileName = "RangedAbility", menuName = "Ability/NewRangedAbility")]
public class RangedAbility : Ability
{
    [SerializeField] public float _damagePercent;
    [SerializeField] private Stats _damageStat;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private GameObject _projectilePrefab; 
    public float DamagePercent => _damagePercent;
    public Stats DamageStat => _damageStat;
    public override void Activate(Player owner)
    {
        Projectile projectile = Instantiate(_projectilePrefab, owner.transform.position+owner.transform.forward, owner.transform.rotation).GetComponent<Projectile>();
        StatHandler stats = owner.GetStats();
        int damage = (int)_damagePercent * stats.GetValue(_damageStat) / 100;
        projectile.Init(_projectileSpeed, damage, owner);
    }

    public override string GetDescription()
    {
        return $"{_type.ToString()}\n Shoots a projectile that deals {_damagePercent}% of {_damageStat} damage to collided enemy. Consume {_manacost} mana. Cooldown: {_cooldown} seconds.";
    }
}
