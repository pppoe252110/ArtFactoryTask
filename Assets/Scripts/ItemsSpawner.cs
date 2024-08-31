using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private FallingItem[] _items;
    
    [SerializeField] private int _spawnCount = 5;
    
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _spawnRadius = 2f;

    private List<FallingItem> _spawnedItems;
    private bool _spawned;
    private void Spawn()
    {
        _spawnedItems = new List<FallingItem>();
        for (int i = 0; i < _spawnCount; i++)
        {
            var spawned = Instantiate(_items[Random.Range(0, _items.Length)],
                _spawnPosition.position + (Vector3)Random.insideUnitCircle * _spawnRadius,
                Quaternion.identity * Quaternion.Euler(0, 0, Random.value * 360f));

            spawned.Proceed();
            
            _spawnedItems.Add(spawned);
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            if (!_spawned)
                Spawn();
            _spawned = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }

    }

    private void OnDrawGizmos()
    {
        if(_spawnPosition)
            Gizmos.DrawWireSphere(_spawnPosition.position, _spawnRadius);
    }

    public float GetSleepings()
    {
        if (_spawnedItems==null || _spawnedItems.Count <= 0)
            return 0;

        var sleeping = 0;
        
        foreach (var item in _spawnedItems)
        {
            if (item.IsSleeping)
            {
                sleeping++;
            }
        }
        return sleeping / (float)_spawnedItems.Count;
    }
}
