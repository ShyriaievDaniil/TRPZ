using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "MeleeAbility", menuName = "Ability/NewMeleeAbility")]
public class MeleeAttack : Ability
{
    [SerializeField] public float _damagePercent;
    [SerializeField] private Stats _damageStat;
    public float DamagePercent => _damagePercent;
    public Stats DamageStat => _damageStat;

    public override void Activate(Player owner)
    {
        EventBus.OnAttacking(owner);
        Collider[] colliders = Physics.OverlapBox(owner.transform.position+owner.transform.forward*2+Vector3.up, 
            new Vector3(1.5f, 2, 1.5f), owner.transform.rotation, 1<<3);
        if (colliders.Any())
        {
            int damage = (int)_damagePercent * 100 / 100;
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.GetComponent<IHealth>() != (IHealth)owner)
                {
                    collider.gameObject.GetComponent<IHealth>().TakeDamage(damage);
                }
            }
        }
    }
}
