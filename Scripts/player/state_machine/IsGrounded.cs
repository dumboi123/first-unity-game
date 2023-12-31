
using UnityEngine;

public class PlayerIsGrounded : PlayerBaseState
{
    public PlayerIsGrounded(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
        InitializeSubState();
     }
    public override void EnterState() {
        _isRootState = true;
        _ctx._doublejump = true;
        if(_ctx._movePressed)
            _ctx._animState = PlayerStateManager.MovementStates.walk;
        else
            _ctx._animState = PlayerStateManager.MovementStates.idle;
    }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void ExitState() {}
    public override void CheckSwitchState()
    {
        switch (_ctx.Walled())
        {
            case false:
                if (_ctx.JumpGetButton())
                {
                    _ctx.Jump();
                    SwitchState(_factory.InSpace());
                }
                else if (!_ctx.Grounded() && _ctx.GetVelocityY() < -.1f){
                    SwitchState(_factory.InSpace());
                }
            break;
            case true:
                if (_ctx.JumpGetButton() && !_ctx._movePressed)
                {
                    _ctx.Jump();
                    SwitchState(_factory.InSpace());
                }
                else if (!_ctx.Grounded() && _ctx.GetVelocityY() < -.1f){
                    SwitchState(_factory.InSpace());
                }
            break;
        }
            
    }
    public override void InitializeSubState()
    {
        if(_ctx._movePressed) SetSubState(_factory.Walk());
        else SetSubState(_factory.Idle());
    }
}
