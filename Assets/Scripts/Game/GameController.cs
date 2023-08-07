using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool _spawnEnemies;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    private ObjectPool objectPool;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        GameObject playerGameObject = Instantiate(_playerPrefab);
        playerGameObject.SetActive(true);
        playerGameObject.GetComponent<PlayerController>().Init();

        
        objectPool = new ObjectPool(enemyPrefab, 20, camera.transform, false, 80, true);
        objectPool.Init(playerGameObject.transform);
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
            objectPool.GetObjectFromPool();
        }
        
    }

   
}
