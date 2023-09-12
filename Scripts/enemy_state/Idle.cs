using UnityEngine;

public class Idle : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy) 
    {
        enemy.ischasing = false;
        enemy.IdleCoroutine();
    }
    public override void UpdateState(EnemyStateManager enemy) 
    {
        if (!enemy.chased)
        {
            if (enemy.chasedistance <= enemy.chasezone)
                enemy.SwitchState(enemy.ChaseState);
            else
                enemy.SwitchState(enemy.PatrolState);
        }
        else
            enemy.SwitchState(enemy.ReturnState);
    }
   

}
