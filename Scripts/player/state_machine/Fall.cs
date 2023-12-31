
using UnityEngine;

public class PlayerFall : PlayerBaseState
{
    public PlayerFall(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
        InitializeSubState();
     }
    public override void EnterState() {
        _ctx._animState = PlayerStateManager.MovementStates.fall;
     }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void ExitState() { }
    public override void CheckSwitchState(){

        if (_ctx.JumpGetButtonDown() && _ctx._doublejump)
            SwitchState(_factory.DoubleJump());
        else if (_ctx.Walled() && _ctx._movePressed)
            SwitchState(_factory.WallSlide());        
     }
    public override void InitializeSubState()
    {
        if(_ctx._movePressed) SetSubState(_factory.Walk());
        else SetSubState(_factory.Idle());
    }
    
}