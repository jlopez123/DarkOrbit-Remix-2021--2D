using UnityEngine;

public static class GameObjectExtenseions
{
    public static void CleanPooledObjects(this GameObject gameObject)
    {
        foreach (var pooledObject in gameObject.GetComponentsInChildren<PooledMonoBehaviour>())
        {
            pooledObject.BackToPoolTransform();
        }
    }
}