using Unity.VisualScripting;
using UnityEngine;

public class PlayerInSpace : PlayerBaseState
{
    public PlayerInSpace(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
        InitializeSubState();
     }
    public override void EnterState() {
        _isRootState = true;
        if(_ctx.GetVelocityY() < -.1f)
            _ctx._animState = PlayerStateManager.MovementStates.fall;
        else if (_ctx.GetVelocityY() > .1f)
            _ctx._animState = PlayerStateManager.MovementStates.jump;
     }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void ExitState() { }
    public override void CheckSwitchState() {
        if (_ctx.Grounded()){
            _ctx.IsWallSliding = false;
            SwitchState(_factory.IsGrounded());
        }
     }
    public override void InitializeSubState()
    {
        if(_ctx.JumpGetButtonDown()) 
            SetSubState(_factory.DoubleJump());
        else if(_ctx.GetVelocityY() <-.1f)
            SetSubState(_factory.Fall());
        else if ((_ctx._currentMoveInput!=0 || _ctx.GetVelocityY() < -.1f) && _ctx.Walled())
            SetSubState(_factory.WallSlide());
        else if(_ctx.GetVelocityY() >.1f)
            SetSubState(_factory.Jump());
    }
}