using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InterfaceProvider<T>
{
    [SerializeField]
    private ScriptableObject _interfaceObject;

    private T _interface;

    public T Value
    {
        get
        {
            if (_interface == null)
            {
                _interface = _interfaceObject as T;
            }
            return _interface;
        }
    }
}
