using UnityEngine;

public class PlayerService
{
    public Transform Transform { get => _playerTransform; }
    public Vector3 Position { get => _playerTransform.position; }
    public Vector3 LocalPosition { get => _playerTransform.localPosition; }
    public Vector3 Forward { get => _playerTransform.forward; }
    public Vector3 Right { get => _playerTransform.right; }


    private Transform _playerTransform;

    public void SetPlayer(PlayerView player) => _playerTransform = player.transform;
}