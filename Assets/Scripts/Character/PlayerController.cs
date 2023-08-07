using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _health;
    private IMovement playerMovement;
    private Rigidbody2D rigidbody2D;

    public void Init()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerMovement = new Movement(3.0f, 50.0f, -100.0f, transform, 0.0f);
    } 


    public void FixedUpdate() 
    {
       if(playerMovement != null && rigidbody2D != null) playerMovement.Move(rigidbody2D);
    }

    public void GetDirectionWrapped(InputAction.CallbackContext callbackContext) 
    {
        if(playerMovement != null) playerMovement.GetMovementDirection<InputAction.CallbackContext>(callbackContext);
    }

}
