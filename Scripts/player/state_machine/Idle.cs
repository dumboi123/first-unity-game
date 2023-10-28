using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerBaseState
{
    public PlayerIdle(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) :base(currentContext,playerStateFactory) { }
    public override void EnterState() { }
    public override void UpdateState() {
        CheckSwitchState();
    }
    public override void CheckSwitchState() { }
    public override void ExitState() { }
}
