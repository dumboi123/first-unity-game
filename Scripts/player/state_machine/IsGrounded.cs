using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsGrounded : PlayerBaseState
{
    public PlayerIsGrounded(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() {
        _ctx._doublejump = true;
    }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void CheckSwitchState()
    {
        if (_ctx.JumpGetButton())
            SwitchState(_factory.Jump());
    }
    public override void ExitState() { }
}
