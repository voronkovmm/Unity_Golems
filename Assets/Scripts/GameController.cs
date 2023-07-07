using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Waypoint waypoint;

    private void Awake()
    {
        InitializePlayer();
        InitializeGlobalEvent();
    }

    private void InitializePlayer()
    {
        PlayerAsset playerAsset = Resources.Load<PlayerAsset>(ResourcesPath.Player);

        Transform playerTransform = Instantiate(playerAsset.PlayerPrefab, waypoint.FirstWaypoint.Position, Quaternion.identity);
        playerTransform.LookAt(waypoint.SecondWaypoint.Position);
        PlayerView playerView = playerTransform.GetComponent<PlayerView>();
        playerView.Waypoint = waypoint;
        
        GameService.Singleton.CameraCinemachine.SetCameraForTarget(playerView.CameraHolder);
        GameService.Singleton.PlayerService.SetPlayer(playerView);
    }
    private void InitializeGlobalEvent() => new GlobalEvent();
}

