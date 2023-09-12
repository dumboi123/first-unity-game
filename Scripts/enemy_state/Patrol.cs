using UnityEngine;
public class Patrol : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy) 
    {

    }
    public override void UpdateState(EnemyStateManager enemy) 
    {
        if(enemy.chasedistance > enemy.chasezone)
            enemy.Patrolling();
        else
        {
            enemy.SwitchState(enemy.ChaseState);
        }
    }

}
