using UnityEngine;

public class Chaser : MonoBehaviour
{
    private Mover _mover;
    private Rotator _rotator;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
    }

    public void Activate(Player player)
    {
        float chasePosition = player.transform.position.x;
        float direction = Mathf.Sign(chasePosition - transform.position.x);

        _rotator.Rotate(direction);
        _mover.MoveOnX(direction);
    }
}
