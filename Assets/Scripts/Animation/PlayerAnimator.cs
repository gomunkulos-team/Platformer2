using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private readonly int Speed = Animator.StringToHash(nameof(Speed));
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ControlAnimation(float speed)
    {
        _animator.SetFloat(Speed, Mathf.Abs(speed));
    }
}