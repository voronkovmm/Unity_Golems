using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    private Stack<Bullet> Bullets = new();
    private Weapon _weapon;
    private WeaponAsset _weaponAsset;

    public BulletPool(Weapon weapon, WeaponAsset weaponAsset)
    {
        _weapon = weapon;
        _weaponAsset = weaponAsset;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.SetActive(false);
        Bullets.Push(bullet);
    }

    public Bullet GetBullet(Vector3 spawnPos, Vector3 targetPos)
    {
        if (Bullets.Count == 0) AddBullet();

        Bullet bullet = Bullets.Pop();
        bullet.GetReady(spawnPos, targetPos);

        return bullet;
    }

    private void AddBullet()
    {
        Bullet bullet = Object.Instantiate(_weaponAsset.BulletPrefab);
        bullet.Construct(_weapon, _weaponAsset, this);
        bullet.SetActive(false);
        Bullets.Push(bullet);
    }
}