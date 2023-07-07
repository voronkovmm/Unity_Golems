using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [HideInInspector] public Waypoint Waypoint;
    public Transform CameraHolder => _cameraHolder;
    [SerializeField] private Transform _cameraHolder;
    private PlayerPresenter _playerPresenter;

    private void Start() => _playerPresenter = new PlayerPresenter(this);
    private void Update() => _playerPresenter.Update();
}

