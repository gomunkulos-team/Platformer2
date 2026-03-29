using UnityEngine;

public class Folover : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;

    private void Update()
    {
        Vector3 ofsetPosition = new Vector3(_offsetX, _offsetY);
        transform.position = _followTarget.position + ofsetPosition;
    }
}
