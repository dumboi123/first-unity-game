using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsGrounded : PlayerBaseState
{
    public PlayerIsGrounded(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() { }
    public override void UpdateState() { }
    public override void ExitState() { }
}
