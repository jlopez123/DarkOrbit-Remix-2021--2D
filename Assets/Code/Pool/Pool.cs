using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Dictionary<PooledMonoBehaviour, Pool> Pools = new Dictionary<PooledMonoBehaviour, Pool>();

    private PooledMonoBehaviour _prefab;

    private Queue<PooledMonoBehaviour> _objects = new Queue<PooledMonoBehaviour>();

    public static Pool GetPool(PooledMonoBehaviour prefab)
    {
        if (Pools.Count > 0 && Pools.ContainsKey(prefab))
            return Pools[prefab];

        return CreateNewPool(prefab);
    }
    public static void ClearPools()
    {
        Pools = new Dictionary<PooledMonoBehaviour, Pool>();
    }
    public T Get<T>() where T : PooledMonoBehaviour
    {
        if (_objects.Count == 0)
            GrowPool(_prefab.AmountToGrowPool);

        var pooledObject = _objects.Dequeue();

        return pooledObject as T;
    }
    private static Pool CreateNewPool(PooledMonoBehaviour prefab)
    {
        var newPool = new GameObject("Pool " + prefab.name).AddComponent<Pool>();
        newPool._prefab = prefab;
        newPool.GrowPool(prefab.InitialPoolSize);
        Pools.Add(prefab, newPool);
        return newPool;
    }

    private void GrowPool(int amountToGrow)
    {
        for (int i = 0; i < amountToGrow; i++)
        {
            var newObject = Instantiate(_prefab, this.transform);
            newObject.name += i;
            newObject.OnReturnPool += ReturnToAvailablePool;
            newObject.gameObject.SetActive(false);
        }
    }
    private void ReturnToAvailablePool(PooledMonoBehaviour obj)
    {
        _objects.Enqueue(obj);
    }

    private void OnDestroy()
    {
        Pools.Remove(_prefab);
    }
}
