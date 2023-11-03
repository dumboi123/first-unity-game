using Unity.VisualScripting;
using UnityEngine;

public class PlayerInSpace : PlayerBaseState
{
    public PlayerInSpace(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
        InitializeSubState();
     }
    public override void EnterState() {
        _isRootState = true;
        Debug.Log("spacing");
        // if(_ctx.JumpGetButton())
        //     _ctx.Jump();
        // else if(_ctx.GetVelocityY()< -.1f)
        //     _ctx._animState = PlayerStateManager.MovementStates.fall;
        //_ctx.Jump();
        //_ctx._animState = PlayerStateManager.MovementStates.jump;
     }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void ExitState() { }
    public override void CheckSwitchState() {
        if (_ctx.Grounded())
            SwitchState(_factory.IsGrounded());
     }
    public override void InitializeSubState()
    {
        //SetSubState(_factory.DoubleJump());
        if(_ctx.JumpGetButton() && _ctx.Grounded()) 
            SetSubState(_factory.Jump());
        else if(_ctx.GetVelocityY() <-.1f )
            SetSubState(_factory.Fall());
    }
    
}