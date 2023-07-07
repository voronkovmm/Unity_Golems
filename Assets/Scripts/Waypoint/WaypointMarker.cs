using UnityEngine;

public class WaypointMarker : MonoBehaviour
{
    public Vector3 Position { get => transform.localPosition; }
    public Quaternion Rotation { get => transform.rotation; }
    public Vector3 Forward { get => transform.forward; }

    [SerializeField] public int Number;
}
