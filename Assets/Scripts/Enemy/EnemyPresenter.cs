using UnityEngine.AI;

public class EnemyPresenter
{
    private EnemyModel _model;
    private EnemyView _view;
    private EnemyBehaviour _behaviour;
    private NavMeshAgent _navMeshAgent;
    
    public EnemyPresenter(EnemyView enemyView)
    {
        _view = enemyView;
        _model = new EnemyModel(this);
        _navMeshAgent = _view.GetComponent<NavMeshAgent>();
        
        GlobalEvent.WaypointAchieved += GlobalEvent_WaypointAchieved;
    }

    public void Update() => _behaviour.Update();
    public void OnAnimatorMove() => _behaviour.OnAnimatorMove();

    private void GlobalEvent_WaypointAchieved(int numberWaypoint)
    {
        if (numberWaypoint != _model.WaypointMarker.Number) return;

        _behaviour.State = EnemyState.MoveToPlayer;

        _model.PositionIndex = GlobalEvent.InvokeNewEnemy(_view);
    }
    public void CreateBehaviour(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.StoneGolem:
                _behaviour = new MeleeEnemyBehaviour(_navMeshAgent, this, _model);
                break;
            case EnemyType.Tower:
                _behaviour = new TowerBehaviour(_navMeshAgent, this, _model);
                break;
        }
    }


    public void OnDeath()
    {
        GlobalEvent.InvokeDieEnemy(_model.PositionIndex);
        _view.Death();
    }
    public void OnApplyDamage(float currentHealth) => _view.OnApplyDamage(currentHealth);
    public void SetMaxHealth(float maxHealth) => _view.SetMaxHealth(maxHealth);


    public void ApplyDamage(float damage) => _model.HealthDamage(damage);
    public int GetIndex() => _model.PositionIndex;
    public void UpdatePositionIndex(int index) => _model.PositionIndex = index;
    public void SetEnemyMarker(EnemyMarker enemyMarker) => _model.EnemyMarker = enemyMarker;
    public void SetEnemyAsset(EnemyAsset enemyAsset) => _model.EnemyAsset = enemyAsset;
    public EnemyType GetEnemyType() => _model.EnemyType;
}