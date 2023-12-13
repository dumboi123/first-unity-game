public class TrunkPatrol : TrunkBaseState
{
    public TrunkPatrol(TrunkStateManager currentContext, TrunkStateFactory TrunkStateFactory) :base(currentContext,TrunkStateFactory) { }
    public override void EnterState()
    {
        _ctx._animState =TrunkStateManager.MovementStates.patrol;
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void CheckSwitchState()
    {
        if(_ctx.WallDetect() || !_ctx.GroundDetect())
            SwitchState(_factory.Idle());
        else if (_ctx.PlayerDetect())
            SwitchState(_factory.Shoot());  
    }

}