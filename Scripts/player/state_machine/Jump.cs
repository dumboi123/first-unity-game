using UnityEngine;

public class PlayerJump : PlayerBaseState
{
    public PlayerJump(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
        InitializeSubState();
     }
    public override void EnterState() {
        Debug.Log("jump");
        _ctx._animState = PlayerStateManager.MovementStates.jump;
    }
    public override void UpdateState() {
       CheckSwitchState();
    }
    public override void ExitState() {}
    public override void CheckSwitchState() {

        if (_ctx.GetVelocityY() < -.1f)
            SwitchState(_factory.Fall());
        else if (_ctx.JumpGetButtonDown() && _ctx._doublejump)
            SwitchState(_factory.DoubleJump());
        else if (_ctx.Walled() && (_ctx._movePressed || _ctx.GetVelocityY() < -.1f))
            SwitchState(_factory.WallSlide());
        
    }
    public override void InitializeSubState()
    {
        if(_ctx._movePressed) SetSubState(_factory.Walk());
        else SetSubState(_factory.Idle());
    }
    
}
