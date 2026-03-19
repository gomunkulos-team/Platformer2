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

    private bool _playerWasSeen = false;
    private bool _seePlayerNow = false;
    private Coroutine _playerLostCoroutine;

    public event Action<Player> PlayerSpotted;
    public event Action PlayerLost;

    private void Update()
    {
        _poinOfView = new Vector2(transform.position.x, _eyePosition.position.y);
        _direction = transform.right;

        _hit = Physics2D.Raycast(_poinOfView, _direction, _viewDistance, _layerMask);

        if (_hit && _hit.collider.TryGetComponent(out Player player))
        {
            _seePlayerNow = true;

            if (_playerWasSeen == false)
            {
                PlayerSpotted?.Invoke(player);
                Debug.Log("Event");
            }

            if (_playerLostCoroutine != null)
            {
                StopCoroutine(_playerLostCoroutine);
                _playerLostCoroutine = null;
            }

            Debug.DrawRay(_poinOfView, _direction * _viewDistance, Color.red);
        }
        else 
        {
            _seePlayerNow= false;

            if (_playerWasSeen && _playerLostCoroutine == null)
                _playerLostCoroutine = StartCoroutine(LoosePlayerAfterTime());

            Debug.DrawRay(_poinOfView, _direction * _viewDistance, Color.cyan);
        }

        _playerWasSeen = _seePlayerNow;
    }

    private IEnumerator LoosePlayerAfterTime()
    {
        Debug.Log("Coroutine Start");
        yield return new WaitForSecondsRealtime(_chaseTime);
        _playerLostCoroutine = null;
        PlayerLost?.Invoke();
    }
}
