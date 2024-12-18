using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyData : ISpawnable
{
    public int maxHealth;
    public int damage;
    public int currentHealth;
    public int speed;
    public bool isAttacking = false;
    public string type;

    public EnemyData(int maxHealth, int speed, int damage)
    {
        this.maxHealth = maxHealth;
        this.speed = speed;
        this.damage = damage;
        currentHealth = maxHealth;
        type = "Skeleton_Minion";
    }
    public virtual ISpawnable Clone()
    {
        return new EnemyData(maxHealth, speed, damage);
    }
    
    public virtual IEnumerator Attack(Transform targetPosition, Enemy owner)
    {
        IHealth target = targetPosition.GetComponent<IHealth>();
        while (true)
        {
            if ((targetPosition.position - owner.transform.position).magnitude <= 2)
            {
                isAttacking = true;
                EventBus.OnAttacking(owner);
                yield return new WaitForSeconds(0.5f);
                DealDamage(target, owner);
                yield return new WaitForSeconds(0.7f);
                isAttacking = false;
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    protected virtual void DealDamage(IHealth target, Enemy owner)
    {
        Collider[] colliders = Physics.OverlapBox(owner.transform.position + owner.transform.forward * 2 + Vector3.up,
            new Vector3(1, 2, 1), owner.transform.rotation, 1 << 3);
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
}
