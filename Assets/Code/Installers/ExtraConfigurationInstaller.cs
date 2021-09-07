using UnityEngine;

public class ExtraConfigurationInstaller : Installer
{
    [SerializeField]
    private RankIconsConfiguration _rankIconsConfig;
    [SerializeField]
    private CompanyIconsConfiguration _companyIconsConfig;
    [SerializeField]
    private RewardsConfiguration _rewardsConfiguration;
    public override void Install(ServiceLocator serviceLocator)
    {
        var extraConfigService = new ExtraConfigurationsService(Instantiate(_rankIconsConfig), Instantiate(_companyIconsConfig), Instantiate(_rewardsConfiguration));
        ServiceLocator.Instance.RegisterService<ExtraConfigurationsService>(extraConfigService);
    }
}
