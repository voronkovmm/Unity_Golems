using UnityEngine;

[CreateAssetMenu(fileName = "New Player Asset", menuName = "MyAssets/Player")]
public class PlayerAsset : ScriptableObject
{
    public float MovementSpeed = 3f;
    public float RotateSpeed = 2f;
    public float ForceJump = 8f;
    public float SpeedJump = 0.2f;

    public Transform PlayerPrefab;
}
