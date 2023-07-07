using Cinemachine;
using UnityEngine;

public class CameraCinemachine : MonoBehaviour
{
    public void SetCameraForTarget(Transform target)
    {
        CinemachineVirtualCamera cinemachine = GetComponent<CinemachineVirtualCamera>();
        cinemachine.Follow = target;
    }
}
