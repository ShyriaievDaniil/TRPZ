using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerEnemyData : EnemyData
{
    private EnemyData _minionData;

    public NecromancerEnemyData(int maxHealth, int speed, int damage) : base(maxHealth, speed, damage)
    {
        _minionData = new EnemyData(10, 5, 5);
        type = "Skeleton_Mage";
    }
    public override IEnumerator Behaviour(Transform targetPosition, Enemy owner)
    {
        IHealth target = targetPosition.GetComponent<IHealth>();
        while (true)
        {
            if ((targetPosition.position - owner.transform.position).magnitude <= 5)
            {
                yield return Attack(target, owner);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public override IEnumerator Attack(IHealth target, Enemy owner)
    {
        isAttacking = true;
        EventBus.OnAttacking(owner);
        yield return new WaitForSeconds(1f);
        EventBus.OnSummon(_minionData, owner);
        isAttacking = false;
        yield return new WaitForSeconds(2f);
    }

    public override ISpawnable Clone()
    {
        return new NecromancerEnemyData(maxHealth, speed, damage);
    }
}
