using System.Collections.Generic;
using UnityEngine.AI;

public class PlayerStateMachine
{
    public IState CurrentState { get; private set; }
    
    public PlayerPresenter _presenter;
    private PlayerMovementState _movementState;
    private PlayerAttackState _attackState;
    private PlayerJumpState _jumpState;
    private Dictionary<State, IState> _states;

    public enum State { Movement, Attack, Jump }

    public PlayerStateMachine(NavMeshAgent navMeshAgent, PlayerModel model)
    {
        _movementState = new PlayerMovementState(this, navMeshAgent, model);
        _attackState = new PlayerAttackState(this, navMeshAgent);
        _jumpState = new PlayerJumpState(this, navMeshAgent, model);

        AddStates();
    }

    public void Initialize() => TransitionTo(State.Movement);

    public void TransitionTo(State state)
    {
        var newState = _states[state];

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void Update() => CurrentState?.Update();

    private void AddStates()
    {
        _states = new Dictionary<State, IState>
        {
            { State.Movement, _movementState },
            { State.Attack, _attackState },
            { State.Jump, _jumpState }
        };
    }
}


