using UnityEngine;

public class EnemyWalkStrategy : WalkStrategy
{
    private Transform _target;

    public EnemyWalkStrategy(CharacterController characterController, Transform transform, float movementSpeed, Component owner) :
    base(characterController, transform, movementSpeed, owner)
    {
    }

    public override void Look()
    {
        if (_target)
        {
            _transform.LookAt(new Vector3(_target.position.x, _transform.position.y, _target.position.z));
        }
    }

    protected override void GetDirection()
    {
        if (_target)
        {
            Vector3 directionToEnemy = (_target.position - _transform.position).normalized;
            _direction = new Vector3(directionToEnemy.x, _velocity, directionToEnemy.z);
        }
        else
        {
            _direction = new Vector3(0, _velocity, 0);
        }
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
