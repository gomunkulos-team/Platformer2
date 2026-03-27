using System;
using System.Collections;
using UnityEngine;

public class AlarmSistem : MonoBehaviour
{
    [SerializeField] private float _viewDistance;
    [SerializeField] private Transform _eyePosition;
    [SerializeField] private LayerMask _layerMask;

    private float _chaseTime = 2;

    private bool _playerWasSeen = false;
    private Coroutine _playerLostCoroutine;

    public event Action<Player> PlayerSpotted;
    public event Action PlayerLost;

    private void Update()
    {
        RaycastHit2D hit;
        Vector2 poinOfView;
        Vector2 direction;

        bool seePlayerNow = false;

        poinOfView = new Vector2(transform.position.x, _eyePosition.position.y);
        direction = transform.right;

        hit = Physics2D.Raycast(poinOfView, direction, _viewDistance, _layerMask);

        if (hit && hit.collider.TryGetComponent(out Player player))
        {
            seePlayerNow = true;

            if (_playerWasSeen == false)
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
            seePlayerNow = false;

            if (_playerWasSeen && _playerLostCoroutine == null)
                _playerLostCoroutine = StartCoroutine(LoosePlayerAfterTime());

            Debug.DrawRay(poinOfView, direction * _viewDistance, Color.cyan);
        }

        _playerWasSeen = seePlayerNow;
    }

    private IEnumerator LoosePlayerAfterTime()
    {
        yield return new WaitForSecondsRealtime(_chaseTime);
        _playerLostCoroutine = null;
        PlayerLost?.Invoke();
    }
}
