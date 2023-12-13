public class TrunkShoot : TrunkBaseState
{
    public TrunkShoot(TrunkStateManager currentContext, TrunkStateFactory TrunkStateFactory) :base(currentContext,TrunkStateFactory) { }
    public override void EnterState()
    {
        _ctx.IsIdle = true;
        _ctx._animState =TrunkStateManager.MovementStates.shoot;
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void CheckSwitchState()
    {
        if(_ctx.IsStopShooting)
        {
            _ctx.IsStopShooting = false;
            _ctx._continue = true;
            SwitchState(_factory.Idle());

        }
    }

}