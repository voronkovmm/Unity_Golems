using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFactory
{
    private Dictionary<EnemyType, EnemyAsset> _enemies = new();

    public EnemyFactory() => Load();

    public void Create(Vector3 pos, EnemyMarker enemyMarker)
    {
        Transform enemy = Object.Instantiate(_enemies[enemyMarker.EnemyType].Prefab, pos, Quaternion.identity);
        enemy.LookAt(enemyMarker.Waypoint.Position);
        EnemyView enemyView = enemy.GetComponent<EnemyView>();
        enemyView.SetEnemyMarker(enemyMarker);
        enemyView.SetEnemyAsset(_enemies[enemyMarker.EnemyType]);
        enemyView.SetRankColor(GameService.Singleton.EnemyMaterials.GetRankMaterial(enemyMarker.EnemyRank));
    }

    private void Load()
        => Resources.LoadAll<EnemyAsset>(ResourcesPath.Enemies)
        .ToList()
        .ForEach(x => _enemies.Add(x.Type, x));
}