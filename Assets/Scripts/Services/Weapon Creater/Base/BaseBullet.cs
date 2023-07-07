using UnityEngine;

public class BaseBullet : Bullet
{
    private Vector3 _moveDirection;
    private float _timerLife;

    protected override void OnEnableBullet()
    {
        _timerLife = 2;
        _moveDirection = (Target - Transform.position).normalized;
    }

    protected override void TriggerLogic(Collider other)
    {
        if(other.TryGetComponent(out IDamageable component))
        {
            component.ApplyDamage(WeaponAsset.Damage);
        }
        
        BulletPool.ReturnBullet(this);
    }

    protected override void UpdateLogic()
    {
        _timerLife -= Time.deltaTime;
        
        if (_timerLife < 0)
            BulletPool.ReturnBullet(this);

        Transform.Translate(_moveDirection * 20 * Time.deltaTime);
    }
}
