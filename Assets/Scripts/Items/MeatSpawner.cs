using UnityEngine;

public class MeatSpawner : MonoBehaviour
{
    [SerializeField] private FirstAid _firstAidPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private void Awake()
    {
        int minIndex = 0;
        int maxIndex = _spawnPoints.Length;

        int randomIndex = Random.Range(minIndex, maxIndex);

        _firstAidPrefab.transform.position = _spawnPoints[randomIndex].position;

        Instantiate(_firstAidPrefab);
    }
}
