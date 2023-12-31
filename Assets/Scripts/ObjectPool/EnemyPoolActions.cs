using UnityEngine;
using ObjectPool;

public class EnemyPoolActions : MonoBehaviour,IPoolActions
{
    [SerializeField] private Transform _startTransform;
    [SerializeField] private int _offset;
    public void GetAction(GameObject @object)
    {
        SetPrefabStartPosition(@object);
        @object.SetActive(true);
    }

    public GameObject Preload(GameObject prefab) {
        GameObject instantiatedObject = Object.Instantiate(prefab);
        
        instantiatedObject.SetActive(false);
        return instantiatedObject;
}

    public void ReturnAction(GameObject @object)
    {
        if(@object != null)@object.SetActive(false);
    }
    private void SetPrefabStartPosition(GameObject obj)
    {
        if ( obj != null && _startTransform != null)
        {
            obj.transform.localPosition = new Vector3(_startTransform.position.x + Utilities.RandomSignInt(_offset),
            _startTransform.position.y + Utilities.RandomSignInt(_offset), obj.transform.position.z);
        }
    }

    
}
