using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform _firePoint;
    protected Transform FirePoint => _firePoint;
    protected WeaponAsset WeaponAsset;
    protected BulletPool BulletPool;

    private void Start()
    {
        BulletPool = new BulletPool(this, WeaponAsset);
    }

    public abstract void Fire(Vector3 targetPos);
    public void SetWeaponAsset(WeaponAsset weaponAsset) => WeaponAsset = weaponAsset;
}
