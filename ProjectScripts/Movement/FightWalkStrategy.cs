using UnityEngine;

public class FightWalkStrategy : WalkStrategy
{
    private int _layerMask = LayerMask.GetMask("ViewRadius");
    private Camera _camera;

    public FightWalkStrategy(CharacterController characterController, Transform transform, float movementSpeed, Component owner) :
    base(characterController, transform, movementSpeed, owner)
    {
        _camera = Camera.main;
    }

    protected override void GetDirection()
    {
        _direction = new Vector3(Input.GetAxis("Horizontal"), _velocity, Input.GetAxis("Vertical"));
    }

    public override void Look()
    {
        var worldMousePosition = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(worldMousePosition, out hit, 50f, _layerMask))
        {
            _transform.LookAt(new Vector3(hit.point.x, _transform.position.y, hit.point.z));
        }
    }
}
