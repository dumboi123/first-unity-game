
using UnityEngine;

public class ReturnPatrol : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy) 
    {
        enemy.chased = false;
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.chasedistance <= enemy.chasezone)
            enemy.SwitchState(enemy.ChaseState);
        else
        {
            if (enemy.OutRange(enemy.transform, enemy.wp)) enemy.ReturnPatrol();
            else enemy.SwitchState(enemy.PatrolState);
        }
    }


}
