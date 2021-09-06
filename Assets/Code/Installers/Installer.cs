using UnityEngine;

public abstract class Installer : MonoBehaviour
{
    public abstract void Install(ServiceLocator serviceLocator);
}
