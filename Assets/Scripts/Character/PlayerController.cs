using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private IMovement playerMovement;
    //[SerializeField] private IWeapon
    private Rigidbody2D rigidbody2D;

    public void Init()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerMovement = 
    } 


    public void FixedUpdate() 
    {
       if(playerMovement != null && rigidbody2D != null) playerMovement.Value.Move(rigidbody2D);
    }

    public void GetDirectionWrapped(InputAction.CallbackContext callbackContext) 
    {
        if(playerMovement != null) playerMovement.Value.GetMovementDirection<InputAction.CallbackContext>(callbackContext);
    }

    public void Damage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void SetTarget(Transform targetTransform)
    {
        throw new System.NotImplementedException();
    }
}
