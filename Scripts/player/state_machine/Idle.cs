using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerBaseState
{
    public PlayerIdle(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) :base(currentContext,playerStateFactory) { }
    public override void EnterState() {
        Debug.Log("idle state");
        _ctx._input = 0;
        _ctx._animState = PlayerStateManager.MovementStates.idle;
        Debug.Log(_ctx._animState);
     }
    public override void UpdateState() {
        //_ctx._animState = MovementStates.idle;
        CheckSwitchState();
    }
    public override void CheckSwitchState() {
        if(_ctx._movePressed) SwitchState(_factory.Walk());
     }
    public override void ExitState() { }
    public override void InitializeSubState(){}
}
