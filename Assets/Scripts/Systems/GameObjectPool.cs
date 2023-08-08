
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectPool
{
    public class GameObjectPool : PoolBase<GameObject>
    {

        #region Constructor
        public GameObjectPool(
            GameObject prefab,
            int preloadCount,
            IPoolActions poolActions
            ) : base(()=> poolActions.Preload(prefab), poolActions.GetAction, poolActions.ReturnAction, preloadCount)
        {

        }
        #endregion
    }
}
