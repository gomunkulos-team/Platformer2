using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const KeyCode JumpButton = KeyCode.Space;

    private bool _isJump;

    public float DirectionX { get; private set; }

    public bool GetIsJump() => GetBoolAsATrigger(ref  _isJump);

    private void Update()
    {
        DirectionX = Input.GetAxis(Horizontal);

        if(Input.GetKeyDown(JumpButton))
            _isJump = true;
    }

    private bool GetBoolAsATrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
