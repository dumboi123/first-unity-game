
using UnityEngine;

public class Chase : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy) 
    {
        enemy.ischasing = true;
        enemy.chased = true;
    }
    public override void UpdateState(EnemyStateManager enemy) 
    {
        if (enemy.chasedistance <= enemy.chasezone && !enemy.OutRange(enemy.player, enemy.limit))
            enemy.Chasing();
        else
        {
            enemy.SwitchState(enemy.IdleState);
        }
    }

}
