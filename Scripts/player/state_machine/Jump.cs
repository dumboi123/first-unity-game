using UnityEngine;

public class PlayerJump : PlayerBaseState
{
    public PlayerJump(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() {
        _ctx.Jumping();
    }
    public override void UpdateState() {
       CheckSwitchState();
    }
    public override void CheckSwitchState() {
        if (_ctx.Grounded())
        {
            SwitchState(_factory.IsGrounded());
        }
        else if (_ctx.JumpGetButtonDown()  && _ctx._doublejump)
        {
            _ctx.DoubleJump();
            SwitchState(_factory.Jump());
        }
    }
    public override void ExitState() { }
}
