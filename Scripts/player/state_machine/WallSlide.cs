using UnityEngine;
public class PlayerWallSlide : PlayerBaseState
{
    public PlayerWallSlide(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() {
        _ctx.IsWallSliding = true;
        _ctx._doublejump = true;
        _ctx._animState = PlayerStateManager.MovementStates.wallslide;
    }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void CheckSwitchState() {
        if (_ctx.JumpGetButtonDown()){
            _ctx.IsWallSliding = false;
            _ctx.IsWallJump = true;
            SwitchState(_factory.Jump());
        }            
        else if (!_ctx.Walled() || !_ctx._movePressed){
            _ctx.IsWallSliding = false;
            _ctx._input =0;
            SwitchState(_factory.Fall());
        }
    }
    public override void ExitState() { }
    public override void InitializeSubState(){}
}