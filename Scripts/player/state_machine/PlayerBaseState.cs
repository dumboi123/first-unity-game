using Unity.VisualScripting;

public abstract class PlayerBaseState{
    protected bool _isRootState =false;
    protected PlayerStateManager _ctx;
    protected PlayerStateFactory _factory;
    private PlayerBaseState _currentSubState,  _currentSuperState;
    public PlayerBaseState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();
    public void UpdateStates(){
        UpdateState();
        if(_currentSubState != null)
            _currentSubState.UpdateStates();
    }
    protected void SwitchState(PlayerBaseState newState){
        ExitState();
        newState.EnterState();
        if(_isRootState)
            _ctx.CurrentState = newState;
        else if(_currentSuperState != null)
            _currentSuperState.SetSubState(newState);
    }
    protected void SetSubState(PlayerBaseState newSubState){
        newSubState.SetSuperState(this);
        _currentSubState = newSubState;
    }
    protected void SetSuperState(PlayerBaseState newSuperState){
        _currentSuperState = newSuperState;
    }
}