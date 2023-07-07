using System;

public class EnemyModel
{
    public float Health { get; private set; }
    public float CurrentHealth { get; private set; }
    public int PositionIndex
    {
        get => _positionIndex;
        set
        {
            if (_positionIndex != 0)
            {
                _positionIndex = value;
                OnIndexWasChanged?.Invoke();
            }
            else
            {
                _positionIndex = value;
            }
        }
    }
    public EnemyType EnemyType { get; private set; }
    public WaypointMarker WaypointMarker { get; private set; }
    public EnemyMarker EnemyMarker
    {
        get => _enemyMarker;
        set
        {
            _enemyMarker = value;
            WaypointMarker = value.Waypoint;
        }
    }
    public EnemyAsset EnemyAsset 
    {
        get => _enemyAsset;
        set
        {
            _enemyAsset = value;
            LoadDataFromAsset();
        }
    }

    private int _positionIndex;
    private EnemyPresenter _presenter;
    private EnemyAsset _enemyAsset;
    private EnemyMarker _enemyMarker;

    public event Action OnIndexWasChanged;

    public EnemyModel(EnemyPresenter presenter) => _presenter = presenter;

    public void HealthDamage(float damage)
    {
        CurrentHealth -= damage;
        _presenter.OnApplyDamage(CurrentHealth);

        if (CurrentHealth == 0)
            _presenter.OnDeath();

    }

    private void LoadDataFromAsset()
    {
        EnemyType = EnemyAsset.Type;
        CurrentHealth = Health = EnemyAsset.Health;
        _presenter.SetMaxHealth(Health);
        _presenter.CreateBehaviour(EnemyType);
    }
}