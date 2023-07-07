using UnityEngine;

public class Chicken : MonoBehaviour
{
    private const string SWING = "Swing";
    private const string PECK = "Peck";

    private string[] _animations = { SWING, PECK };

    private float _cooldownAnimation;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _cooldownAnimation = Random.Range(2, 8);
    }

    private void Update() => PlayAnimations();

    private void PlayAnimations()
    {
        _cooldownAnimation -= Time.deltaTime;

        if(_cooldownAnimation < 0)
        {
            _cooldownAnimation = Random.Range(2, 8);
            _animator.Play(_animations[Random.Range(0, _animations.Length)]);
        }
    }
}
