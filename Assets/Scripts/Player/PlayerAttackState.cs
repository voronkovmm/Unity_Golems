using UnityEngine.AI;
using UnityEngine;

class PlayerAttackState : IState
{
    private const string WeaponHolder = "Weapon Holder";

    private PlayerStateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private Transform _weaponHolder;
    private Weapon _weapon;

    public PlayerAttackState(PlayerStateMachine stateMachine, NavMeshAgent navMeshAgent)
    {
        _stateMachine = stateMachine;
        _navMeshAgent = navMeshAgent;

        _weaponHolder = _navMeshAgent.transform.Find(WeaponHolder);

        _weapon = GetWeapon();
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if(UserInput.Singleton.Input.Attack())
        {
            _weapon.Fire(UserInput.Singleton.Input.PointAttack());
        }
    }

    private Weapon GetWeapon() => GameService.Singleton.WeaponCreater.Create(WeaponType.Base, _weaponHolder.position, _weaponHolder.rotation, _weaponHolder);
}


