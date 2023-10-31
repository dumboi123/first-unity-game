using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerFall : PlayerBaseState
{
    public PlayerFall(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() {
        _ctx._animState = PlayerStateManager.MovementStates.fall;
     }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void ExitState() { }
    public override void CheckSwitchState() { }
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
    
}