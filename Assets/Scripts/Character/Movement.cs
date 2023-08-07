using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class Movement : IMovement
{
    private float _maxSpeed;
    private float _acceleration = 50;
    private float _deceleration = -100;
    private float _currentSpeed = 0;
    private Vector2 _oldMovementDirection;
    protected Vector2 _movementDirection;
    private Transform _transform;

    public Movement(float maxSpeed, float acceleration, float deceleration, Transform transform, float currentSpeed ) 
    {
        _maxSpeed = maxSpeed;
        _currentSpeed = currentSpeed;
        _acceleration = acceleration;
        _deceleration = deceleration;
        _transform = transform;
    }
    
    public void Move(Rigidbody2D rigidbody2D)
    {
        if(_oldMovementDirection.magnitude < 0.05f || _movementDirection.magnitude < 0.05f) return;
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
        rigidbody2D.velocity = _oldMovementDirection * _currentSpeed;
    }
    

    public void GetMovementDirection<T>(T directionPovider)
    {
        Type directiomnProviderType = typeof(T);
        if (directionPovider != null)
        {
            switch (directionPovider)
            {
                case InputAction.CallbackContext:
                    InputAction.CallbackContext context = (InputAction.CallbackContext)Convert.ChangeType(directionPovider, typeof(InputAction.CallbackContext));
                    _movementDirection = context.ReadValue<Vector2>();
                    break;
                case Transform:
                    Transform targetTransform = (Transform)Convert.ChangeType(directionPovider, typeof(Transform));
                    _movementDirection = (targetTransform.position - _transform.position).normalized;
                    break;
            }
        }
    }
}