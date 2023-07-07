using UnityEngine;

public class MouseInput : AbstractInput
{
    private Vector2 centerScreen;

    public override bool Attack() => UnityEngine.Input.GetMouseButtonDown(0);

    public override Vector3 PointAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo);
        return hitInfo.point;
    }
}
