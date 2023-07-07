using System;
using UnityEngine;

public class PlayerModel
{
    public float MovementSpeed { get; private set; }
    public float ForceJump { get; private set; }
    public float SpeedJump { get; private set; }
    public float RotateSpeed { get; private set; }
    public int CountEnemies { get; private set; }
    public WaypointMarker TargetWaypoint { get; private set; }

    private PlayerPresenter _presenter;

    public PlayerModel(PlayerPresenter presenter)
    {
        _presenter = presenter;

        GetDatas();
    }

    public void NewEnemy() => CountEnemies++;

    public void DieEnemy()
    {
        CountEnemies--;

        if (CountEnemies == 0)
        {
            _presenter.AllEnemiesDied();
        }
    }

    public void SetTargetWaypoint(WaypointMarker waypoint) => TargetWaypoint = waypoint;

    private void GetDatas()
    {
        PlayerAsset data = Resources.Load<PlayerAsset>(ResourcesPath.Player);

        MovementSpeed = data.MovementSpeed;
        ForceJump = data.ForceJump;
        SpeedJump = data.SpeedJump;
        RotateSpeed = data.RotateSpeed;
    }
}

