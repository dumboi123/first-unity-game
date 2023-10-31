

public class PlayerWalk : PlayerBaseState
{
    public PlayerWalk(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() {
        _ctx._animState = PlayerStateManager.MovementStates.run;
     }
    public override void UpdateState() {
        CheckSwitchState();
        _ctx._input = _ctx._currentMoveInput;
    }
    public override void CheckSwitchState() {
        if(!_ctx._movePressed) SwitchState(_factory.Idle());
     }
    public override void ExitState() { }
    public override void InitializeSubState(){}
}
