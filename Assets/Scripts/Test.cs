using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    public float radius = 3;
    public float distance = -0.9f;

    public Transform target;

    [ContextMenu("TestCirclePosition")]
    private void TestCirclePosition()
    {
        Transform playerTransform = GameService.Singleton.PlayerService.Transform;

        for (int i = 0; i < 4; i++)
        {
            Vector3 playerPos = GameService.Singleton.PlayerService.Position;
            Vector3 playerForward = GameService.Singleton.PlayerService.Forward;

            float angle = default;
            switch (i)
            {
                case 0:
                    angle = Mathf.PI * (1 + 1) / (4 + 1);
                    break;
                case 1:
                    angle = Mathf.PI * (2 + 1) / (4 + 1);
                    break;
                case 2:
                    angle = Mathf.PI * (0 + 1) / (4 + 1);
                    break;
                case 3:
                    angle = Mathf.PI * (3 + 1) / (4 + 1);
                    break;
            }

            Vector3 centerPos = playerPos + (new Vector3(playerForward.x, 0, playerForward.z) * distance);
            float z = Mathf.Sin(angle) * radius;
            float x = Mathf.Cos(angle) * radius;
            Vector3 circlePos = new Vector3(x, 0, z);

            circlePos = centerPos + (Quaternion.LookRotation(playerForward) * circlePos);

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = circlePos;
            cube.transform.localScale = new Vector3(1, 2, 1f);
            Destroy(cube, 2f);
        }
    }

    private void Start()
    {
        //StartCoroutine(Rotation());
    }

    IEnumerator Rotation()
    {
        Transform playerTransform = GameService.Singleton.PlayerService.Transform;
        Quaternion start = playerTransform.rotation;
        Quaternion end = Quaternion.LookRotation(target.forward);
        float interval = default;

        while (interval < 1) 
        {
            interval += (Time.deltaTime * 3) / 1;
            playerTransform.rotation = Quaternion.Lerp(start, end, interval);
            yield return null;
        }
    }

}
