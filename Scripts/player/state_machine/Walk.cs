

public class PlayerWalk : PlayerBaseState
{
    public PlayerWalk(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() { }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void CheckSwitchState() { }
    public override void ExitState() { }
}
