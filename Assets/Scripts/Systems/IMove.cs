
using UnityEngine.InputSystem;

public interface IMove 
{
    public void GatherInput(InputActionReference action);
    public void Move();
    
}
