using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<EnemyMarker> markers = new();
    private EnemyFactory _enemyFactory;

    private void Start()
    {
        CreateFactory();
        AddMarkers();
        CreateEnemies();
    }

    private void CreateFactory() => _enemyFactory = new EnemyFactory();

    private void AddMarkers()
    {
        transform
            .GetComponentsInChildren<EnemyMarker>()
            .ToList()
            .ForEach(x => markers.Add(x));
    }

    private void CreateEnemies() => markers.ForEach(x => _enemyFactory.Create(
        pos: x.transform.position,
        enemyMarker: x));
}