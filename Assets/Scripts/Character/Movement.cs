using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour, IMovement
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration = 50;
    [SerializeField] private float _deceleration = 100;
    //[SerializeField] private InputActionReference _movementActionReference;
    private float _currentSpeed = 0;
    private Vector2 _oldMovementDirection;
    protected Vector2 _movementDirection;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //_movementDirection = _movementActionReference.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
       // Move();
    }
    public void Move()
    {
        if (_movementDirection.magnitude > 0 && _currentSpeed >= 0)
        {
            _oldMovementDirection = _movementDirection;
            _currentSpeed += _acceleration * _maxSpeed * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= _deceleration * _maxSpeed * Time.deltaTime;
        }
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
        _rigidbody2D.velocity = _oldMovementDirection * _currentSpeed;
    }


    public void SetDirection(Vector2 direction)
    {
        if(direction != null)_movementDirection = direction;
    }

}