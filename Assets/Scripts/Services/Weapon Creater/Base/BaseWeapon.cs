using UnityEngine;

public class BaseWeapon : Weapon
{
    public override void Fire(Vector3 targetPos)
        => BulletPool
            .GetBullet(FirePoint.position, targetPos);
}


