using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovement
{
    void SetDirection(Vector2 direction);
    void Move();
}