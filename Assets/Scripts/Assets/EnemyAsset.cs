using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Asset", menuName = "MyAssets/Enemy")]
public class EnemyAsset : ScriptableObject
{
    public float Health;
    public EnemyType Type;
    public Transform Prefab;
}