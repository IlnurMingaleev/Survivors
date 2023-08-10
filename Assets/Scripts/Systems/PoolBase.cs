using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class PoolBase<T>
    {
        #region Fields
        private readonly Func<T> _preloadFunc;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;
        private Queue<T> _pool;
        private List<T> _active = new List<T>();
        #endregion
        #region Constructor
        public PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount) 
        {
            _preloadFunc = preloadFunc;
            _getAction = getAction;
            _returnAction = returnAction;
            _pool = new Queue<T>();
            if (preloadFunc == null) 
            {
                Debug.LogError("Preload function is null");
            }
            for (int i = 0; i < preloadCount; i++) 
            {
                Return(preloadFunc());
            }
        }
        #endregion
        #region PublicMethods

        public T Get() 
        {
            T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
            _getAction(item);
            _active.Add(item);
            return item;
        }

        public void Return(T item) 
        {
            _returnAction(item);
            _pool.Enqueue(item);
            _active.Remove(item);
        }

        public void ReturnAll() 
        {
            foreach (T item in _active.ToArray()) 
            {
                Return(item);
            }
        }
        #endregion
    }
}