using UnityEngine.AI;

public class PlayerPresenter
{
    private PlayerModel _playerModel;
    private NavMeshAgent _navMeshAgent;
    private PlayerStateMachine _stateMachine;

    public PlayerPresenter(PlayerView view)
    {
        _playerModel = new PlayerModel(this);
        _navMeshAgent = view.GetComponent<NavMeshAgent>();
        _stateMachine = new PlayerStateMachine(_navMeshAgent, _playerModel);

        GlobalEvent.NewEnemy += GlobalEvent_NewEnemy;
        GlobalEvent.DieEnemy += GlobalEvent_DieEnemy;
        GlobalEvent.StartGame += GlobalEvent_StartGame;
    }

    public void Update() => _stateMachine.Update();
    public void AllEnemiesDied() => _stateMachine.TransitionTo(PlayerStateMachine.State.Movement);

    private void GlobalEvent_StartGame() => _stateMachine.Initialize();
    private void GlobalEvent_DieEnemy() => _playerModel.DieEnemy();
    private void GlobalEvent_NewEnemy() => _playerModel.NewEnemy();

}

