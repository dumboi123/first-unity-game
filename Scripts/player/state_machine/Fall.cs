
using UnityEngine;

public class PlayerFall : PlayerBaseState
{
    public PlayerFall(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
        InitializeSubState();
     }
    public override void EnterState() {
        _isRootState =true;
        _ctx.OutDoubleJump();
        _ctx._animState = PlayerStateManager.MovementStates.fall;
     }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void ExitState() { }
    public override void CheckSwitchState(){
        if(_ctx.Grounded())
            SwitchState(_factory.IsGrounded());
        else if(_ctx.JumpGetButtonDown() && _ctx._doublejump)
            SwitchState(_factory.DoubleJump());
     }
    public override void InitializeSubState()
    {
        if(_ctx._movePressed) SetSubState(_factory.Walk());
        else SetSubState(_factory.Idle());
    }
    
}