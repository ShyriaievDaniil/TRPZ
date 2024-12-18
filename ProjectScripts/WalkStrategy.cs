using UnityEngine;

public abstract class WalkStrategy
{
    [SerializeField] protected float _movementSpeed = 10.0f;
    [SerializeField] protected float _gravity = -5f;
    [SerializeField] protected float _velocity = -0.5f;
    [SerializeField] protected float _rotationSpeed = 10f;
    protected CharacterController _characterController;
    protected Transform _transform;
    protected Component _owner;
    protected bool _isFalling = false;

    public Vector3 _direction;

    public WalkStrategy(CharacterController characterController, Transform transform, float movementSpeed, Component owner)
    {
        _characterController = characterController;
        _transform = transform;
        _movementSpeed = movementSpeed;
        _owner = owner;
    }

    protected abstract void GetDirection();
    public abstract void Look();
    public void Move() {
        DoGravity();
        GetDirection();
        _characterController.Move(_direction * Time.fixedDeltaTime * _movementSpeed);
        EventBus.OnWalking(_owner, (_direction.x != 0 || _direction.z != 0));
    }
    protected void DoGravity()
    {
        if (_characterController.isGrounded)
        {
            if (_isFalling)
            {
                _isFalling = false;
                _velocity = -0.5f;
            }
        }
        else
        {
            _velocity += _gravity * Time.fixedDeltaTime;
            _isFalling = true;
        }
        EventBus.OnFalling(_owner, _isFalling);
    }
}
