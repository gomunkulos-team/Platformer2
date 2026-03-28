using UnityEngine;
using UnityEngine.UIElements;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const KeyCode JumpButton = KeyCode.Space;

    private int _deviceButtonNumber = 0;

    private bool _isJump;
    private bool _isMouseClick;

    public float DirectionX { get; private set; }

    public bool GetIsJump() => GetBoolAsATrigger(ref _isJump);
    public bool GetIsMouseClick() => GetBoolAsATrigger(ref _isMouseClick);

    private void Update()
    {
        DirectionX = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(JumpButton))
            _isJump = true;

        if (Input.GetMouseButtonDown(_deviceButtonNumber))
            _isMouseClick = true;
    }

    private bool GetBoolAsATrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
