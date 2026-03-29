using System;
using System.Collections;
using UnityEngine;

public class AlarmSistem : MonoBehaviour
{
    [SerializeField] private float _viewDistance;
    [SerializeField] private Transform _eyePosition;
    [SerializeField] private LayerMask _layerMask;

    private float _chaseTime = 2;

    private bool _wasPlayerSeen = false;
    private Coroutine _playerLostCoroutine;

    public event Action<Player> PlayerSpotted;
    public event Action PlayerLost;

    private void Update()
    {
        RaycastHit2D hit;
        Vector2 poinOfView;
        Vector2 direction;

        bool isPalyerSeenNow = false;

        poinOfView = new Vector2(transform.position.x, _eyePosition.position.y);
        direction = transform.right;

        hit = Physics2D.Raycast(poinOfView, direction, _viewDistance, _layerMask);

        if (hit && hit.collider.TryGetComponent(out Player player))
        {
            isPalyerSeenNow = true;

            if (_wasPlayerSeen == false)
            {
                PlayerSpotted?.Invoke(player);
            }

            if (_playerLostCoroutine != null)
            {
                StopCoroutine(_playerLostCoroutine);
                _playerLostCoroutine = null;
            }

            Debug.DrawRay(poinOfView, direction * _viewDistance, Color.red);
        }
        else
        {
            isPalyerSeenNow = false;

            if (_wasPlayerSeen && _playerLostCoroutine == null)
                _playerLostCoroutine = StartCoroutine(LoosePlayerAfterTime());

            Debug.DrawRay(poinOfView, direction * _viewDistance, Color.cyan);
        }

        _wasPlayerSeen = isPalyerSeenNow;
    }

    private IEnumerator LoosePlayerAfterTime()
    {
        yield return new WaitForSecondsRealtime(_chaseTime);
        _playerLostCoroutine = null;
        PlayerLost?.Invoke();
    }
}
