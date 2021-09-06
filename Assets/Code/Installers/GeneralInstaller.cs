using UnityEngine;

public abstract class GeneralInstaller : MonoBehaviour
{
    [SerializeField]
    private Installer[] _installers;

    private void Awake()
    {
        InstallDependencies();
    }

    private void Start()
    {
        DoStart();
    }
    private void InstallDependencies()
    {
        foreach (var installer in _installers)
        {
            installer.Install(ServiceLocator.Instance);
        }
        DoInstallDependencies();
    }

    protected abstract void DoInstallDependencies();

    protected abstract void DoStart();
}
