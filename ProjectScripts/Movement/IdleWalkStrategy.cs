using UnityEngine;

public class IdleWalkStrategy : WalkStrategy
{
    public IdleWalkStrategy(CharacterController characterController, Transform transform, float movementSpeed, Component owner) : 
        base(characterController, transform, movementSpeed, owner){}

    protected override void GetDirection()
    {
        _direction = new Vector3(Input.GetAxis("Horizontal"), _velocity, Input.GetAxis("Vertical"));
    }

    public override void Look()
    {
        if (_direction.x != 0 || _direction.z != 0)
        {
            var rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z), Vector3.up);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
