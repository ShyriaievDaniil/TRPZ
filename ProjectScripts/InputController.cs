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
            _player.SwitchWalkStrategy();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _player.Notify(this, "UseAbility", AbilityTypes.LMB);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _player.Notify(this, "UseAbility", AbilityTypes.RMB);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.Notify(this, "UseAbility", AbilityTypes.Space);
        }
    }
}
