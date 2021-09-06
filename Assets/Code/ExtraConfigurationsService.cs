public class ExtraConfigurationsService
{
    public readonly RankIconsConfiguration RankIcons;
    public readonly CompanyIconsConfiguration CompanyIcons;

    public ExtraConfigurationsService(RankIconsConfiguration rankIconsConfiguration, CompanyIconsConfiguration companyIconsConfiguration)
    {
        RankIcons = rankIconsConfiguration;
        CompanyIcons = companyIconsConfiguration;
    }
}