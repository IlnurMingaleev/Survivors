using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void Damage(int damage);
    void SetTarget(Transform targetTransform);
    void GetCurrentHealth();
}
