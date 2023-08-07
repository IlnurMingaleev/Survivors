using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovement
{
    void GetMovementDirection<T>(T directionPovider);
    void Move(Rigidbody2D rigidbody);
}