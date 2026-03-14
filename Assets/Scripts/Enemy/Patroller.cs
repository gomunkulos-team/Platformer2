using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    private int _currentWaypoint = 0;
    private float _distanceTolerance = 0.1f;

    public float Direction {  get; private set; }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - _waypoints[_currentWaypoint].position.x) < _distanceTolerance)
                _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

        Direction = Mathf.Sign(_waypoints[_currentWaypoint].position.x -  transform.position.x);
    }
}
