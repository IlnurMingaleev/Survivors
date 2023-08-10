using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Transform _playerTransform;
    private IHealth _charHealthComponent;
    private IMovement _charMovementComponent;




    public void Awake()
    {
        
        //_charHealthComponent = GetComponent<IHealth>();
        _charMovementComponent = GetComponent<IMovement>();
    }
    private void Update()
    {
        if(_playerTransform != null && _charMovementComponent != null)_charMovementComponent.SetDirection((_playerTransform.position - transform.position).normalized);
    }

    private void FixedUpdate()
    {
        if(_playerTransform != null) Move();
    }

    public void SetTargetTransform(Transform targetTransform) 
    {
        _playerTransform = targetTransform;
    }
    public void Move()
    {
        if (_charMovementComponent != null) _charMovementComponent.Move();
    }


}
