using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;
using Object = UnityEngine.Object;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool _spawnEnemies;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;

    private GameObject _runtimePlayer;

    [SerializeField] private IPoolActionsProvider _poolActionsProvider;
    private GameObjectPool _enemyPool;
    //private ObjectPool objectPool;
    //private Camera camera;

    private void Awake()
    {
        //camera = Camera.main;
        _runtimePlayer = Object.Instantiate(_playerPrefab);
        _enemyPool = new GameObjectPool(_enemyPrefab, 20, _poolActionsProvider.Value);
        //GameObject playerGameObject = Instantiate(_playerPrefab);
        //playerGameObject.SetActive(true);
        //playerGameObject.GetComponent<PlayerController>().Init();

        
        //objectPool = new ObjectPool(enemyPrefab, 20, camera.transform, false, 80, true);
        //objectPool.Init(playerGameObject.transform);
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (_spawnEnemies) 
        {
            yield return new WaitForSeconds(_spawnInterval);
            EnemyController enemyController = _enemyPool.Get().GetComponent<EnemyController>();
            enemyController.SetTargetTransform(_runtimePlayer.transform);
        }
        
    }

   
}
