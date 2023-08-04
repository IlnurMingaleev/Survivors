using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour, IMove, ITakeDamage, IDealDamage
{
    private DataCharacter characterData;

    public virtual void DealDamage()
    {
        throw new System.NotImplementedException();
    }

    public virtual void GatherInput(InputActionReference action)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Move()
    {
        throw new System.NotImplementedException();
    }

    public virtual void TakeDamage()
    {
        throw new System.NotImplementedException();
    }

}
