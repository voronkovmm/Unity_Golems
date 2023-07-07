using UnityEngine;

public class TouchInput : AbstractInput
{
    public override bool Attack()
    {
        return false;
    }

    public override Vector3 PointAttack()
    {
        throw new System.NotImplementedException();
    }
}
