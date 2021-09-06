using System.Collections.Generic;
using UnityEngine;

public class PoolCreator : MonoBehaviour
{
    [SerializeField]
    private List<PooledMonoBehaviour> _prefabsToPool;

    private void Awake()
    {
        foreach (PooledMonoBehaviour prefab in _prefabsToPool)
        {
            var newPool = Pool.GetPool(prefab);
            newPool.transform.SetParent(transform);
        }
    }
}
