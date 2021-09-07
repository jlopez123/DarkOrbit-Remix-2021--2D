public class ExtraConfigurationsService
{
    public readonly RankIconsConfiguration RankIcons;
    public readonly CompanyIconsConfiguration CompanyIcons;
    public readonly RewardsConfiguration RewardsConfiguration;

    public ExtraConfigurationsService(RankIconsConfiguration rankIconsConfiguration, CompanyIconsConfiguration companyIconsConfiguration, RewardsConfiguration rewardsConfiguration)
    {
        RankIcons = rankIconsConfiguration;
        CompanyIcons = companyIconsConfiguration;
        RewardsConfiguration = rewardsConfiguration;
    }
}