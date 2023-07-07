using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehaviour
{
    public EnemyState State;
    protected NavMeshAgent navMeshAgent; 
    protected Transform playerTransform; 
    protected EnemyPresenter presenter;
    protected EnemyModel model;
    protected Animator animator;
    protected Transform transform;
    
    protected EnemyBehaviour(NavMeshAgent navMeshAgent, EnemyPresenter presenter, EnemyModel model)
    {
        this.navMeshAgent = navMeshAgent;
        this.presenter = presenter;
        this.model = model;
        playerTransform = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        transform = navMeshAgent.transform;
        animator = navMeshAgent.GetComponent<Animator>();
        State = EnemyState.Idle;    
    }

    public void Update() => UpdateBehaviour();
    public abstract void OnAnimatorMove();

    protected abstract void IdleBehaviour();
    protected abstract void MoveToPlayerBehaviour();
    protected abstract void AttackBehaviour();

    private void UpdateBehaviour()
    {
        switch (State)
        {
            case EnemyState.Idle:
                IdleBehaviour();
                break;
            case EnemyState.MoveToPlayer:
                MoveToPlayerBehaviour();
                break;
            case EnemyState.Attack:
                AttackBehaviour();
                break;
        }
    }
}