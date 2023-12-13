
using UnityEngine;

public class TrunkIdle : TrunkBaseState
{
    public TrunkIdle(TrunkStateManager currentContext, TrunkStateFactory TrunkStateFactory) :base(currentContext,TrunkStateFactory) { }
    public override void EnterState()
    {
        _ctx.IsIdle = true;
        _ctx._animState =TrunkStateManager.MovementStates.idle;
        _ctx.StartIdleCoroutine();
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void CheckSwitchState()
    {
        if(!_ctx.IsIdle)
            SwitchState(_factory.Patrol());
        else if (_ctx.PlayerDetect())
            SwitchState(_factory.Shoot());
            
    }

}