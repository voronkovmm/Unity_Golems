using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Вынести константы анимации
public class MeleeEnemyBehaviour : EnemyBehaviour
{
    private readonly string ANIMATION_MOVE_BLEND = "Move Blend";
    private readonly string ANIMATION_ATTACK_CLAWS = "Attack Claws";
    private readonly string ANIMATION_MOVE_SPEED = "Move Speed";
    private readonly float radiusDestination = 2.5f;
    private readonly float distanceDestination = 0.5f;
    private readonly float distanceWaiting = 7;
    private readonly float widthWaitingZone = 4;
    private readonly float lengltWaitingZone = 3;
    
    private readonly float attackCooldown = 2f;
    private float timerAttack;

    private readonly bool _needDebug = true;

    public MeleeEnemyBehaviour(NavMeshAgent navMeshAgent, EnemyPresenter presenter, EnemyModel model) : base(navMeshAgent, presenter, model)
    {
        navMeshAgent.updatePosition = false;

        animator.SetFloat(ANIMATION_MOVE_SPEED, Random.Range(1f, 1.25f));

        model.OnIndexWasChanged += PositionIndexWasChanged;
    }

    protected override void AttackBehaviour()
    {
        timerAttack += Time.deltaTime;
        if (timerAttack > attackCooldown)
        {
            animator.Play(ANIMATION_ATTACK_CLAWS);
            timerAttack = 0;
        }
    }
    protected override void IdleBehaviour() { }
    protected override void MoveToPlayerBehaviour()
    {   
        if (!navMeshAgent.hasPath && !navMeshAgent.pathPending)
        {
            navMeshAgent.destination = GetDestination();
            animator.SetBool("isMove", true);
        }
        else if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            navMeshAgent.velocity = Vector3.Lerp(Vector3.zero, navMeshAgent.velocity, navMeshAgent.remainingDistance / navMeshAgent.stoppingDistance);

            if (navMeshAgent.velocity.magnitude < 0.5f)
            {
                animator.SetBool("isMove", false);
                navMeshAgent.ResetPath();
                State = EnemyState.NoState;
                transform.GetComponent<EnemyView>().StartCoroutine(EndMovePhase());
            }
        }

        animator.SetFloat(ANIMATION_MOVE_BLEND, navMeshAgent.velocity.magnitude);
    }
    public override void OnAnimatorMove()
    {
        Vector3 rootPosition = animator.rootPosition;
        navMeshAgent.nextPosition = rootPosition;
        rootPosition.y = navMeshAgent.nextPosition.y;
        navMeshAgent.transform.position = rootPosition;
    }

    private void PositionIndexWasChanged()
    {
        if (navMeshAgent.hasPath)
            navMeshAgent.ResetPath();

        State = EnemyState.MoveToPlayer;
    }
    private Vector3 GetDestination()
    {
        Vector3 playerPos = GameService.Singleton.PlayerService.Position;
        Vector3 playerForward = GameService.Singleton.PlayerService.Forward;

        Debug.Log(presenter.GetIndex());
        float angle;
        switch (presenter.GetIndex())
        {
            case 0:
                angle = Mathf.PI * (1 + 1) / (4 + 1);
                break;
            case 1:
                angle = Mathf.PI * (2 + 1) / (4 + 1);
                break;
            case 2:
                angle = Mathf.PI * (0 + 1) / (4 + 1);
                break;
            case 3:
                angle = Mathf.PI * (3 + 1) / (4 + 1);
                break;
            default:
                return GetRandomPositionInWaitingZone();
        }

        Vector3 centerPos = playerPos + (new Vector3(playerForward.x, 0, playerForward.z) * distanceDestination);
        float z = Mathf.Sin(angle) * radiusDestination;
        float x = Mathf.Cos(angle) * radiusDestination;
        Vector3 circlePos = new Vector3(x, 0, z);

        circlePos = centerPos + (Quaternion.LookRotation(playerForward) * circlePos);

        if (_needDebug) DebugPosDrawLine(navMeshAgent.transform.position, circlePos, presenter.GetIndex());

        return circlePos;
    }
    private Vector3 GetRandomPositionInWaitingZone()
    {
        float distanceMultiple = distanceDestination * distanceWaiting;
        Vector3 center = playerTransform.position + playerTransform.forward * distanceMultiple;
        Quaternion quat = Quaternion.LookRotation(playerTransform.forward);

        if(_needDebug)
        {
            Debug.DrawLine(center, playerTransform.position, Color.red, 2000);
            Debug.DrawLine(center + quat * new Vector3(-widthWaitingZone, 0, 0), center + quat * new Vector3(-widthWaitingZone, 0, lengltWaitingZone), Color.red, 2000);
            Debug.DrawLine(center + quat * new Vector3(widthWaitingZone, 0, 0), center + quat * new Vector3(widthWaitingZone, 0, lengltWaitingZone), Color.red, 2000);
            Debug.DrawLine(center + quat * new Vector3(-widthWaitingZone, 0, lengltWaitingZone), center + quat * new Vector3(widthWaitingZone, 0, lengltWaitingZone), Color.red, 2000);
            Debug.DrawLine(center + quat * new Vector3(-widthWaitingZone, 0, 0), center + quat * new Vector3(widthWaitingZone, 0, 0), Color.red, 2000);
        }

        return center + quat * new Vector3(Random.Range(-widthWaitingZone, widthWaitingZone), 0, Random.Range(0, lengltWaitingZone));
    }
    private IEnumerator EndMovePhase()
    {
        Vector3 playerPos = playerTransform.position;
        Quaternion start = transform.rotation;
        Vector3 direction = playerPos - transform.position;
        direction.y = 0;
        Quaternion end = Quaternion.LookRotation(direction);
        float step = default;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        while (step < 1)
        {
            step += Time.deltaTime * 1.5f;
            transform.rotation = Quaternion.Lerp(start, end, step);
            yield return null;
        }

        if (presenter.GetIndex() == IndexEnemy.NOT_AWAILABLE)
        {
            State = EnemyState.Idle;
            yield break;
        }
        else
        {
            timerAttack = attackCooldown - 0.2f;
            State = EnemyState.Attack;
        }
    }
    private void DebugPosDrawLine(Vector3 start, Vector3 end, int Index)
    {
        if (Index == IndexEnemy.NOT_AWAILABLE) return;

        Color[] colors =
        {
            Color.red,
            Color.blue,
            Color.yellow,
            Color.black,
        };

        Debug.DrawLine(start, end, colors[Index], 2000);
    }
}
