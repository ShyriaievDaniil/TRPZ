using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class WarriorEnemyData : EnemyData
{
    public WarriorEnemyData(int maxHealth, int speed, int damage) : base(maxHealth, speed, damage)
    {
        type = "Skeleton_Warrior";
    }

    public override IEnumerator Behaviour(Transform targetPosition, Enemy owner)
    {
        IHealth target = targetPosition.GetComponent<IHealth>();
        while (true)
        {
            if ((targetPosition.position - owner.transform.position).magnitude <= 3)
            {
                yield return Attack(target, owner);
            }
            yield return new WaitForSeconds(3f);
        }
    }
    public override IEnumerator Attack(IHealth target, Enemy owner)
    {
        isAttacking = true;
        EventBus.OnAttacking(owner);
        yield return new WaitForSeconds(1.0f);
        DealDamage(target, owner);
        yield return new WaitForSeconds(0.6f);
        DealDamage(target, owner);
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
    }
    protected override void DealDamage(IHealth target, Enemy owner)
    {
        Collider[] colliders = Physics.OverlapSphere(owner.transform.position,
                2.5f, 1 << 3);
        if (colliders.Any())
        {
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.GetComponent<IHealth>() == target)
                {
                    target.TakeDamage(damage);
                }
            }
        }
    }
    public override ISpawnable Clone()
    {
        return new WarriorEnemyData(maxHealth, speed, damage);
    }
}
