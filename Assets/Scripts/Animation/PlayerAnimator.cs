using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    private readonly int _speed = Animator.StringToHash("Speed");
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ControlAnimation(float speed)
    {
        _animator.SetFloat(_speed, Mathf.Abs(speed));
    }
}