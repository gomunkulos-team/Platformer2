using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private float _timeForRespwanCoin = 3;

    private int _poolCapacity = 5;
    private int _poolMaxSize = 10;

    private Queue<Vector3> _spawnPointList;
    private ObjectPool<Coin> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_coinPrefab),
            actionOnGet: (coin) => SpawnCoin(coin),
            actionOnRelease: (coin) => coin.gameObject.SetActive(false),
            actionOnDestroy: (coin) => Destroy(coin.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );

        _spawnPointList = new Queue<Vector3>();

        foreach(Transform point in _spawnPoints)
        {
            _spawnPointList.Enqueue(point.position);
            GetCoin();
        }
    }

    private void GetCoin()
    {
        _pool.Get();
    }

    private void SpawnCoin(Coin coin)
    {
        coin.Collected += ReleaseCoin;
        coin.transform.position = _spawnPointList.Dequeue();
        coin.gameObject.SetActive(true);
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSecondsRealtime(_timeForRespwanCoin);
        GetCoin();
    }

    private void ReleaseCoin(Coin coin)
    {
        _spawnPointList.Enqueue(coin.transform.position);
        coin.Collected -= ReleaseCoin;
        _pool.Release(coin);
        StartCoroutine(WaitTime());
    }
}
