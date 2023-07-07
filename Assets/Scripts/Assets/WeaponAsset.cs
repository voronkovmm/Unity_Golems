using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Asset", menuName = "MyAssets/Weapon")]
public class WeaponAsset : ScriptableObject
{
    public Weapon WeaponPrefab;
    public Bullet BulletPrefab;
    public WeaponType WeaponType;
    public float Damage;
    public float Speed;
}