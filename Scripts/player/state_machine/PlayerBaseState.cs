public abstract class PlayerBaseState{
    protected PlayerStateManager _ctx;
    protected PlayerStateFactory _factory;
    public PlayerBaseState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();

    public abstract void ExitState();
    void SwitchState(PlayerBaseState newState){
        ExitState();
        newState.EnterState();
        _ctx.CurrentState = newState;
    }
}