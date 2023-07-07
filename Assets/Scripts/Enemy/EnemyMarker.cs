using UnityEngine;

public class EnemyMarker : MonoBehaviour
{
    public EnemyType EnemyType;
    public EnemyRank EnemyRank;
    public WaypointMarker Waypoint;

    private void OnDrawGizmos()
    {
        switch (EnemyRank)
        {
            case EnemyRank.Green:
                Gizmos.color = Color.green; break;
            case EnemyRank.Blue:
                Gizmos.color = Color.blue; break;
            case EnemyRank.Red:
                Gizmos.color = Color.red; break;
        }
        
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.color = Color.white;
    }
}
