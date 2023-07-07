using UnityEngine;

public class GameService : MonoBehaviour
{
    public static GameService Singleton { get; private set; }
    public WeaponCreater<WeaponAsset> WeaponCreater { get; private set; }
    public PlayerService PlayerService { get; private set; }
    public IndexEnemy IndexEnemy { get; private set; }
    public CameraCinemachine CameraCinemachine { get => _cameraCinemachine; }

    [SerializeField] public EnemyMaterials EnemyMaterials;
    [SerializeField] private CameraCinemachine _cameraCinemachine;

    private void Awake()
    {
        InitializeSingleton();
        CreateServices();
    }

    private void CreateServices()
    {
        WeaponCreater = new WeaponCreater<WeaponAsset>();
        PlayerService = new PlayerService();
        IndexEnemy = new IndexEnemy();
    }

    private void InitializeSingleton()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(gameObject);
    }
}