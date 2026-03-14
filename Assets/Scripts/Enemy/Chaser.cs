using UnityEngine;

public class Chaser : MonoBehaviour
{
    private Vector3 _chasePosition;

    public float Direction {  get; private set; }

    private void Update()
    {
        Direction = Mathf.Sign(_chasePosition.x - transform.position.x);
    }

    public void GetPlayerPosition(Player player)
    {
        _chasePosition = player.transform.position;
    }
}
