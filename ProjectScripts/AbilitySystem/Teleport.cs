using UnityEngine;
[CreateAssetMenu(fileName = "TeleportAbility", menuName = "Ability/NewTeleportAbility")]
public class Teleport : Ability
{
    [SerializeField] private float _distance; 
    public override void Activate(Player owner)
    {
        CharacterController characterController = owner.GetComponent<CharacterController>();
        characterController.excludeLayers |= 1 << LayerMask.NameToLayer("Entity");
        characterController.Move(owner.transform.forward*_distance);
        characterController.excludeLayers &= ~(1 << LayerMask.NameToLayer("Entity"));
    }

    public override string GetDescription()
    {
        return $"{_type.ToString()}\n Teleports player forward. Consume {_manacost} mana. Cooldown: {_cooldown} seconds.";
    }
}
