using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsGrounded : PlayerBaseState
{
    public PlayerIsGrounded(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
        InitializeSubState();
     }
    public override void EnterState() {
        _isRootState = true;
        _ctx._doublejump = true;
        _ctx._animState = PlayerStateManager.MovementStates.idle;
        Debug.Log("Isgrounded state : "+_ctx._animState);
    }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void ExitState() {}
    public override void CheckSwitchState()
    {
        if (_ctx.JumpGetButton())
            SwitchState(_factory.Jump());
    }
    public override void InitializeSubState()
    {
        if(_ctx._movePressed) SetSubState(_factory.Walk());
        else SetSubState(_factory.Idle());
    }
}
