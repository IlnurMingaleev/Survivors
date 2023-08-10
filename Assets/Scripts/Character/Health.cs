using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    public void DecreaseHealth(int damage)
    {
        _health = (_health - damage <= 0)? 0: _health - damage;
        
    }

    public float GetHealthProportion()
    {
        return _health /_maxHealth;
    }
}
