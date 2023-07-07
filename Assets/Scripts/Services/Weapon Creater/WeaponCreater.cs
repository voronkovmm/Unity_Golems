using UnityEngine;

public class WeaponCreater<Asset> where Asset : WeaponAsset
{
    private WeaponFactory<Asset> _weaponFactory;

    public WeaponCreater() => _weaponFactory = new WeaponFactory<Asset>(ResourcesPath.Weapons);

    public Weapon Create(WeaponType type, Vector3 position, Quaternion rotation, Transform parent)
        => _weaponFactory.Create(type, position, rotation, parent);
}