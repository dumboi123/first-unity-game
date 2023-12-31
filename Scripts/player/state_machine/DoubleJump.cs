using UnityEngine;

public class PlayerDoubleJump : PlayerBaseState
{
    public PlayerDoubleJump(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
        InitializeSubState();
     }
    public override void EnterState() {
        _ctx.Jump();
        _ctx.DoubleJump();
     }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void ExitState() { }
    public override void CheckSwitchState() {

        if(_ctx.GetVelocityY() < -.1f)
            SwitchState(_factory.Fall());
        else if(_ctx.Walled() && _ctx._movePressed)
            SwitchState(_factory.WallSlide());
            
     }
    public override void InitializeSubState()
    {
        if(_ctx._movePressed) SetSubState(_factory.Walk());
        else SetSubState(_factory.Idle());
    }
    
}