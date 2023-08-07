
using UnityEngine.InputSystem;

public interface IMove 
{
    public void GatherInput(InputAction.CallbackContext context);
    public void Move();
    
}
