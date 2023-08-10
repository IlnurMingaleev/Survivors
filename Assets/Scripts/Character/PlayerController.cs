using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _movementReference;
    private IHealth _charHealthComponent;
    private IMovement _charMovementComponent;
    private Rigidbody2D _rigidbody2D;
    public void Update() 
    {
        if(_charMovementComponent!=null)
            _charMovementComponent.SetDirection(_movementReference.action.ReadValue<Vector2>());
    }

    public void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //_charHealthComponent = GetComponent<IHealth>();
        _charMovementComponent = GetComponent<IMovement>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        if ( _charMovementComponent != null) _charMovementComponent.Move();
    }
}
