public abstract class TrunkBaseState
{
    protected TrunkStateManager _ctx;
    protected TrunkStateFactory _factory;

    public TrunkBaseState ( TrunkStateManager currentContext, TrunkStateFactory trunkStateFactory)
    {
        _ctx = currentContext;
        _factory = trunkStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void CheckSwitchState();

    protected void SwitchState(TrunkBaseState newState){
        newState.EnterState();
        _ctx.CurrentState = newState;
    }
}