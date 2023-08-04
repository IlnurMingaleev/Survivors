using System;
using UnityEngine.InputSystem;

public class Player : Character
{
    //using actions through player because enemy doesnt need actions.
    //And I have data layer only for character parent class.
    //But I think this action references also should be in a data layer script.
    //And I assume that we could place InputactionReference through dependency injection.

    private InputActionReference movement, attack;

    //Weapon is used only for player.
    private Weapon playerWeapon;


    private void OnEnable()
    {
        attack.action.performed += PerformAttack;

    }
    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }
    private void PerformAttack(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }


}
