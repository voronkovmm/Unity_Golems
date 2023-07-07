using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour, IDamageable
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private GameObject _meshRendererGameObject;

    private EnemyPresenter _presenter;


    private void Awake() => _presenter = new EnemyPresenter(this);
    private void Update() => _presenter.Update();
    private void OnAnimatorMove() => _presenter.OnAnimatorMove();


    public void Death() => gameObject.SetActive(false);
    public void OnApplyDamage(float currentHealth) => _healthBar.value = currentHealth;
    public void SetMaxHealth(float maxHealth) => _healthBar.value = _healthBar.maxValue = maxHealth;
    public void SetRankColor(Material rankMaterial)
    {
        switch (_presenter.GetEnemyType())
        {
            case EnemyType.StoneGolem:
                _meshRendererGameObject.GetComponent<SkinnedMeshRenderer>().material = rankMaterial;
                break;
            case EnemyType.Tower:
                Material copyMaterial = _meshRendererGameObject.GetComponent<MeshRenderer>().material;
                Material[] materials = _meshRendererGameObject.GetComponent<MeshRenderer>().materials;
                materials[0] = copyMaterial;
                materials[1] = rankMaterial;
                _meshRendererGameObject.GetComponent<MeshRenderer>().materials = materials;
                break;
        }
    }

    public void UpdatePositionIndex(int index) => _presenter.UpdatePositionIndex(index);
    public void ApplyDamage(float damage) => _presenter.ApplyDamage(damage);
    public void SetEnemyMarker(EnemyMarker enemyMarker) => _presenter.SetEnemyMarker(enemyMarker);
    public void SetEnemyAsset(EnemyAsset enemyAsset) => _presenter.SetEnemyAsset(enemyAsset);
}
