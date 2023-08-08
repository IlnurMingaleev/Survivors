using UnityEngine;

namespace ObjectPool
{
    public interface IPoolActions
    {
        void GetAction(GameObject @object);
        GameObject Preload(GameObject prefab);
        void ReturnAction(GameObject @object);
    }
}