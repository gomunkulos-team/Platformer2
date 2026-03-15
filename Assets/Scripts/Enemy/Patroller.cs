using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    private Mover _mover;
    private Rotator _rotator;

    private int _currentWaypoint = 0;
    private float _distanceTolerance = 0.1f;
    private float _direction;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
    }


    public void Activate()
    {
        if (Mathf.Abs(transform.position.x - _waypoints[_currentWaypoint].position.x) < _distanceTolerance)
                _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

        _direction = Mathf.Sign(_waypoints[_currentWaypoint].position.x -  transform.position.x);

        _rotator.Rotate(_direction);
        _mover.MoveOnX(_direction);
    }
}
