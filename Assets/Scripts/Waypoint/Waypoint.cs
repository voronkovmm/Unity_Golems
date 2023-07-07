using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<WaypointMarker> Waypoints { get; private set; } = new();
    public WaypointMarker FirstWaypoint { get => Waypoints[0]; }
    public WaypointMarker SecondWaypoint { get => Waypoints[1]; }

    private void Awake() => AddWaypoints();

    private void AddWaypoints()
        => transform
            .GetComponentsInChildren<WaypointMarker>()
            .ToList()
            .ForEach(x => Waypoints.Add(x));
}

