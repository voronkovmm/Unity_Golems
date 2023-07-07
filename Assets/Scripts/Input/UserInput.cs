using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static UserInput Singleton { get; private set; }
    public AbstractInput Input { get; private set; } = new MouseInput();

    private void Awake() => InitializeSingleton();

    private void InitializeSingleton()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(this);
    }

}
