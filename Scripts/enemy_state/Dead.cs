using UnityEngine;

public class Dead : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.freeze = true;
    }

    public override void UpdateState(EnemyStateManager enemy) 
    {
        enemy.SetDead();
    }

}
