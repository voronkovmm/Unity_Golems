
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class WeaponFactory<Asset> where Asset : WeaponAsset
{
    private Dictionary<WeaponType, Asset> _weaponAssets;

    public WeaponFactory(string pathToWeaponAssets)
    {
        LoadWeapons(pathToWeaponAssets);
    }

    public Weapon Create(WeaponType type, Vector3 position, Quaternion rotation, Transform parent)
    {
        Weapon weapon = Object.Instantiate<Weapon>(_weaponAssets[type].WeaponPrefab, position, rotation, parent);
        weapon.SetWeaponAsset(_weaponAssets[type]);

        return weapon;
    }

    private void LoadWeapons(string pathToWeaponAssets)
    {
        _weaponAssets = Resources.LoadAll<Asset>(pathToWeaponAssets).ToDictionary(key => key.WeaponType);
    }
}