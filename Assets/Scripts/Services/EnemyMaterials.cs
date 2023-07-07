using UnityEngine;

public class EnemyMaterials : MonoBehaviour
{
    [SerializeField] private Material Green;
    [SerializeField] private Material Blue;
    [SerializeField] private Material Red;

    public Material GetRankMaterial(EnemyRank rank)
    {
        switch (rank)
        {
            case EnemyRank.Red:
                return Red;
            case EnemyRank.Blue:
                return Blue;
            default:
                return Green;
        }
    }
}