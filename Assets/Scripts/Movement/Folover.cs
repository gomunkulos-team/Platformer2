using UnityEngine;

public class Folover : MonoBehaviour
{
    [SerializeField] Transform _followTarget;
    [SerializeField] float _offsetX;
    [SerializeField] float _offsetY;

    private void FixedUpdate()
    {
        Vector3 ofsetPosition = new Vector3(_offsetX, _offsetY);
        transform.position = _followTarget.position + ofsetPosition;
    }
}
