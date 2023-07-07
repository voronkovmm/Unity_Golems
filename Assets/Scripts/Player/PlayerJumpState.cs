using System.Collections;
using UnityEngine;
using UnityEngine.AI;

class PlayerJumpState : IState
{
    private PlayerStateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private Transform _transform;
    private PlayerModel _model;

    public PlayerJumpState(PlayerStateMachine stateMachine, NavMeshAgent navMeshAgent, PlayerModel model)
    {
        _stateMachine = stateMachine;
        _navMeshAgent = navMeshAgent;
        _transform = _navMeshAgent.transform;
        _model = model;
    }

    public void Enter()
    {
        _navMeshAgent.autoTraverseOffMeshLink = false;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.updatePosition = false;

        _transform.GetComponent<PlayerView>().StartCoroutine(StepOne_Jump());
    }

    public void Exit()
    {
        _navMeshAgent.autoTraverseOffMeshLink = true;
        _navMeshAgent.updateRotation = true;
        _navMeshAgent.updateUpAxis = true;
    }

    public void Update() { }

    private IEnumerator StepOne_Jump()
    {
        OffMeshLinkData linkData = _navMeshAgent.currentOffMeshLinkData;
        Vector3 startPosition = _navMeshAgent.transform.position;
        Vector3 endPosition = new Vector3(linkData.endPos.x, linkData.endPos.y + _transform.localScale.y, linkData.endPos.z);
        Quaternion startRotation = _transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(_model.TargetWaypoint.Position - _transform.position);
        float timeJump = _model.SpeedJump * Vector3.Distance(startPosition, endPosition);
        float heightJump = _model.ForceJump * timeJump;

        float step = default;
        float yOffset;
        Vector3 destination;

        while (step < 1)
        {
            step += Time.deltaTime / timeJump;
            yOffset = heightJump * (step - step * step);

            destination = Vector3.Lerp(startPosition, endPosition, step) + yOffset * Vector3.up;
            _navMeshAgent.nextPosition = destination;
            destination.y = _navMeshAgent.nextPosition.y;
            _transform.position = destination;
            _transform.rotation = Quaternion.Lerp(startRotation, endRotation, step);
            yield return null;
        }

        _navMeshAgent.velocity = Vector3.zero;
        _navMeshAgent.CompleteOffMeshLink();
        _navMeshAgent.updatePosition = true;
        _transform.GetComponent<PlayerView>().StartCoroutine(StepTwo_RotationXZtoZero());
    }
    private IEnumerator StepTwo_RotationXZtoZero()
    {
        float step = default;
        Vector3 targetPos = _navMeshAgent.destination;
        targetPos.y = _transform.position.y;
        Quaternion start = _transform.rotation;
        Quaternion end = Quaternion.LookRotation(targetPos - _transform.position);

        while (step < 1) 
        {
            step += Time.deltaTime * 2 / 1;
            _transform.rotation = Quaternion.Lerp(start, end, step);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        _stateMachine.TransitionTo(PlayerStateMachine.State.Movement);
    }
}


