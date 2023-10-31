using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : PlayerBaseState
{
    public PlayerDead(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() { }
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
