using System.Collections;
using System.Linq;
using UnityEngine;

public class BossEnemyDecorator : EnemyData
{
    private EnemyData _base;
    private EnemyWalkStrategy _jumpMover;
    private CharacterController _characterController;

    public BossEnemyDecorator(int maxHealth, int speed, int damage, EnemyData baseEnemy) : base(maxHealth, speed, damage)
    {
        _base = baseEnemy;
        type = _base.type;
        this.maxHealth += _base.maxHealth;
        this.speed += _base.speed;
        this.damage += _base.damage;
        currentHealth = this.maxHealth;
    }
    public override ISpawnable Clone()
    {
        return new BossEnemyDecorator(maxHealth - _base.maxHealth, speed - _base.speed, damage - _base.damage, _base);
    }
    public override IEnumerator Behaviour(Transform targetPosition, Enemy owner)
    {
        IHealth target = targetPosition.GetComponent<IHealth>();
        while (true)
        {
            if ((targetPosition.position - owner.transform.position).magnitude >= 6)
            {
                yield return Jump(targetPosition, owner, target);
            }
            else
            {
                yield return _base.Attack(target, owner);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator Jump(Transform targetPosition, Enemy owner, IHealth target)
    {
        isAttacking = true;
        EventBus.OnJumping(owner);
        if (_jumpMover == null)
        {
            _characterController = owner.GetComponent<CharacterController>();
            _jumpMover = new EnemyWalkStrategy(_characterController, owner.transform, this.speed * 1.5f, owner);
        }
        yield return new WaitForSeconds(1f);
        _jumpMover.SetTarget(targetPosition);
        _jumpMover.SetVelocity(2.5f);
        while (isAttacking)
        {
            _jumpMover.Move();
            if (_characterController.isGrounded)
            {
                isAttacking = false;
                _jumpMover.Move();
            }
            yield return new WaitForFixedUpdate();
        }
        DealDamage(target, owner);
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
}
