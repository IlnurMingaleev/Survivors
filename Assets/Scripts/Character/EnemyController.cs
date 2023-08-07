using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _health,_maxHealth;

    private Transform _playerTransform;
    private SpriteRenderer _spriteRenderer;
    private IMovement _enemyMovement;
    private Rigidbody2D _rigidbody2D;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _enemyMovement = new Movement(2.5f,50.0f,-100.0f,transform,0.0f);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (_playerTransform != null && _enemyMovement != null) _enemyMovement.GetMovementDirection<Transform>(_playerTransform);
    }

    private void FixedUpdate()
    {
        if(_enemyMovement != null) _enemyMovement.Move(_rigidbody2D);
    }

    public void SetTargetTransform(Transform targetTransform) 
    {
        _playerTransform = targetTransform;
    }


 
}
