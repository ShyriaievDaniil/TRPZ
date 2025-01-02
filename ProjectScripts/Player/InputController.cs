using UnityEngine;

public class InputController{
    private Player _player;

    public InputController(Player player)
    {
        _player = player;
    }
    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _player.Notify(this, "SwitchWalkStrategy", null);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _player.Notify(this, "UseAbility", AbilityTypes.LMB);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _player.Notify(this, "UseAbility", AbilityTypes.RMB);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _player.Notify(this, "UseAbility", AbilityTypes.Q);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _player.Notify(this, "UseAbility", AbilityTypes.E);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _player.Notify(this, "UseAbility", AbilityTypes.Shift);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            EventBus.OnLevelUp(_player, 5);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            _player.Notify(this, "AddEquipment", null);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _player.Notify(this, "ToggleInventory", null);
        }
    }
}
