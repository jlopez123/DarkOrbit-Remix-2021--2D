using UnityEngine;

public class ExtraConfigurationInstaller : Installer
{
    [SerializeField]
    private RankIconsConfiguration _rankIconsConfig;
    [SerializeField]
    private CompanyIconsConfiguration _companyIconsConfig;

    public override void Install(ServiceLocator serviceLocator)
    {
        var extraConfigService = new ExtraConfigurationsService(Instantiate(_rankIconsConfig), Instantiate(_companyIconsConfig));
        ServiceLocator.Instance.RegisterService<ExtraConfigurationsService>(extraConfigService);
    }
}
