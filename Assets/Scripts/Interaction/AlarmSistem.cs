using System;
using System.Collections;
using UnityEngine;

public class AlarmSistem : MonoBehaviour
{
    [SerializeField] private float _viewDistance;
    [SerializeField] private Transform _eyePosition;
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit2D _hit;
    private float _chaseTime = 2;
    private Vector2 _poinOfView;
    private Vector2 _direction;

    public event Action<Player> PlayerSpotted;
    public event Action PlayerLost;

    private void Update()
    {
        _poinOfView = new Vector2(transform.position.x, _eyePosition.position.y);
        _direction = transform.right;

        _hit = Physics2D.Raycast(_poinOfView, _direction, _viewDistance, _layerMask);

        if (_hit && _hit.collider.TryGetComponent(out Player player))
        {
            Debug.Log($"Попали в: {_hit.collider.name}  |  tag: {_hit.collider.tag}");

            PlayerSpotted?.Invoke(player);
            StartCoroutine(Chase());
            Debug.DrawRay(_poinOfView, _direction * _viewDistance, Color.red);
        }
        else
        {
            Debug.DrawRay(_poinOfView, _direction * _viewDistance, Color.cyan);
        }
    }

    private IEnumerator Chase()
    {
        yield return new WaitForSecondsRealtime(_chaseTime);

        PlayerLost?.Invoke();
    }
}
