using System;
using System.Collections;
using UnityEngine;

public class PooledMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _initialPoolSize = 10;
    [SerializeField]
    private int _growPoolAmount = 50;

    private Transform _myPoolTransform;
    public int InitialPoolSize => _initialPoolSize;

    public int AmountToGrowPool => _growPoolAmount;

    public event Action<PooledMonoBehaviour> OnReturnPool = delegate { };

    public T Get<T>() where T : PooledMonoBehaviour
    {
        Pool pool = Pool.GetPool(this);
        var pooledObject = pool.Get<T>();
        pooledObject.gameObject.SetActive(true);
        pooledObject._myPoolTransform = pool.transform;
        return pooledObject;
    }
    public T Get<T>(Vector3 position, Quaternion rotation) where T : PooledMonoBehaviour
    {
        var pooledObject = Get<T>();
        pooledObject.transform.position = position;
        pooledObject.transform.rotation = rotation;
        return pooledObject;
    }
    public T Get<T>(Vector3 position, Quaternion rotation, Transform parent) where T : PooledMonoBehaviour
    {
        var pooledObject = Get<T>(position, rotation);
        pooledObject.transform.SetParent(parent);
        return pooledObject;
    }
    protected virtual void OnDisable()
    {
        OnReturnPool(this);
    }

    protected virtual void ReturnToPool(float delay)
    {
        StartCoroutine(ReturnToPoolAfterSeconds(delay));
    }

    private IEnumerator ReturnToPoolAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        BackToPoolTransform();
        gameObject.SetActive(false);
    }

    public void BackToPoolTransform()
    {
        if (transform.parent != _myPoolTransform && _myPoolTransform != null)
            transform.SetParent(_myPoolTransform);
    }
}