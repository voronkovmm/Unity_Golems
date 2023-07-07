using UnityEngine;

//[RequireComponent(typeof(BoxCollider))]
public abstract class Bullet : MonoBehaviour
{
    public Vector3 Target;
    protected Transform Transform;
    protected Weapon Weapon;
    protected BulletPool BulletPool;
    protected WeaponAsset WeaponAsset;

    private void Awake() => Transform = GetComponent<Transform>();
    private void Update() => UpdateLogic();
    private void OnTriggerEnter(Collider other) => TriggerLogic(other);
    private void OnEnable() => OnEnableBullet();

    protected abstract void UpdateLogic();
    protected abstract void TriggerLogic(Collider other);
    protected abstract void OnEnableBullet();

    public void GetReady(Vector3 spawPos, Vector3 targetPos)
    {
        Transform.position = spawPos;
        Target = targetPos;
        SetActive(true);
    }
    public void Construct(Weapon weapon, WeaponAsset weaponAsset, BulletPool bulletPool)
    {
        Weapon = weapon;
        WeaponAsset = weaponAsset;
        BulletPool = bulletPool;
    }
    public void SetActive(bool value) => gameObject.SetActive(value);
}