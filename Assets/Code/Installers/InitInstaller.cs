using System;
using UnityEngine.SceneManagement;

public class InitInstaller : GeneralInstaller
{
    protected override void DoInstallDependencies()
    {

    }

    protected override void DoStart()
    {
        SceneManager.LoadScene(1);
    }
}
