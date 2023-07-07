using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerMovementState : IState
{
    private Queue<WaypointMarker> _waypoints;

    private NavMeshAgent _navMeshAgent;
    private Waypoint _waypoint;
    private PlayerStateMachine _stateMachine;
    private PlayerModel _model;
    private PlayerView _view;
    private Transform _transform;

    public PlayerMovementState(PlayerStateMachine stateMachine, NavMeshAgent navMeshAgent, PlayerModel model)
    {
        _navMeshAgent = navMeshAgent;
        _stateMachine = stateMachine;
        _model = model;

        _view = navMeshAgent.transform.GetComponent<PlayerView>();
        _transform = _view.transform;
        _waypoint = _view.Waypoint;
        _navMeshAgent.speed = model.MovementSpeed;

        AddWaypoints();
    }

    public void Enter() => StartMovement();

    public void Exit() {}

    public void Update()
    {
        if (_navMeshAgent.isOnOffMeshLink)
        {
            _stateMachine.TransitionTo(PlayerStateMachine.State.Jump);
        }
        
        if (!_navMeshAgent.hasPath && !_navMeshAgent.pathPending)
        {
            GlobalEvent.InvokeWaypointAchieved(_waypoints.Dequeue().Number);
            CheckEndWaypoint();
            _stateMachine.TransitionTo(PlayerStateMachine.State.Attack);
        }
    }

    private void AddWaypoints()
    {
        _waypoints = new(_waypoint.Waypoints);
        _waypoints.Dequeue();
    }

    private void StartMovement()
    {
        WaypointMarker waypointMarker = _waypoints.Peek();
        _navMeshAgent.destination = waypointMarker.Position;
        _model.SetTargetWaypoint(waypointMarker);
    }

    private void CheckEndWaypoint()
    {
        if(_waypoints.Count == 0)
        {
            SceneManager.LoadScene(SceneNames.Level_1);
        };
    }
}