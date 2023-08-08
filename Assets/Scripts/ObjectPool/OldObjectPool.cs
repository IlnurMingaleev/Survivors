using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldObjectPool 
{
    private GameObject _prefab;       // The prefab to pool
    private int _initialPoolSize = 10; // Initial number of objects to instantiate
    private bool _canGrow = true; // Whether the pool can dynamically grow if needed
    private bool _spawnOutsideCamView;
    private Transform _spawnTransform;
    private int _offset = 80;
    private List<GameObject> _pooledObjects = new List<GameObject>();

    public OldObjectPool(GameObject prefab, int initialPoolSize, Transform spawnTransform, bool canGrow = false, int offset = 80, bool spawnOutsideCamView = false) 
    {
        _prefab = prefab;
        _initialPoolSize = initialPoolSize;
        _spawnTransform = spawnTransform;
        _canGrow = canGrow;
        _offset = offset;
        _spawnOutsideCamView = spawnOutsideCamView;
    }
    public void Init(Transform playerTransform)
    {
        // Instantiate and initialize the initial pool of objects
        for (int i = 0; i < _initialPoolSize; i++)
        {
            GameObject obj = GameObject.Instantiate(_prefab);
            if(playerTransform != null) obj.GetComponent<EnemyController>().SetTargetTransform(playerTransform);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {

        // Search for an inactive object in the pool and return it
        foreach (GameObject obj in _pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                
                SetPrefabStartPosition(obj);

                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive object found and canGrow is true, create a new one
        if (_canGrow)
        {
            GameObject newObj = GameObject.Instantiate(_prefab);
            _pooledObjects.Add(newObj);
            newObj.SetActive(true);
            return newObj;
        }

        // If no inactive object found and canGrow is false, return null
        return null;
    }

    private void SetPrefabStartPosition(GameObject obj)
    {
        if (_spawnOutsideCamView && obj != null && _spawnTransform != null)
        {
            obj.transform.localPosition = new Vector3(_spawnTransform.position.x + Utilities.RandomSignInt(_offset),
            _spawnTransform.position.y + Utilities.RandomSignInt(_offset), obj.transform.position.z);
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        // Deactivate the object and return it to the pool
        obj.SetActive(false);
    }


    
}
