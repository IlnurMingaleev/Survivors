using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

public class RangedWeapon : Weapon
{
    [SerializeField] private IPoolActionsProvider _poolActionsProvider;
    private GameObjectPool _bulletPool;
    private GameObject _bulletPrefab;

    public void Init() 
    {
        _bulletPool = new GameObjectPool(_bulletPrefab, 10, _poolActionsProvider.Value);
    }
}
