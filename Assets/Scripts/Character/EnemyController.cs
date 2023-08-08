using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _health,_maxHealth;

    [SerializeField]private Transform _playerTransform;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private IMovementProvider _enemyMovement;
    private Rigidbody2D _rigidbody2D;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (_playerTransform != null && _enemyMovement != null) _enemyMovement.Value.GetMovementDirection<Transform>(_playerTransform);
    }

    private void FixedUpdate()
    {
        if(_enemyMovement != null) _enemyMovement.Value.Move(_rigidbody2D);
    }

    public void SetTargetTransform(Transform targetTransform) 
    {
        _playerTransform = targetTransform;
    }


 
}
