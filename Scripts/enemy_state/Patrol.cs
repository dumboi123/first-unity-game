
public class Patrol : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy) 
    {

    }
    public override void UpdateState(EnemyStateManager enemy) 
    {
        if (!enemy.Chasezone())
            enemy.Patrolling();
        else
        {
            enemy.SwitchState(enemy.ChaseState);
        }
    }

}
