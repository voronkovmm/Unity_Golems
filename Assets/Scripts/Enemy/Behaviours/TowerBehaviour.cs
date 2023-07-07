using UnityEngine.AI;

public class TowerBehaviour : EnemyBehaviour
{
    public TowerBehaviour(NavMeshAgent navMeshAgent, EnemyPresenter presenter, EnemyModel model) : base(navMeshAgent, presenter, model)
    {
    }

    public override void OnAnimatorMove()
    {
    }

    protected override void AttackBehaviour()
    {
    }

    protected override void IdleBehaviour()
    {
    }

    protected override void MoveToPlayerBehaviour()
    {
    }
}