using UnityEngine;

public class Rotator : MonoBehaviour
{
    public void Rotate(float direction)
    {
        if (direction < 0)
            TurnLeft();
        else
            TurnRight();
    }

    private void TurnRight()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void TurnLeft()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
