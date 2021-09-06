using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetableInfoInMapUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nicknameText;
    [SerializeField]
    private TextMeshProUGUI _titleText;
    [SerializeField]
    private Image _rankImage;
    [SerializeField]
    private Image _companyImage;
    [SerializeField]
    private Color _allyColor;
    [SerializeField]
    private Color _enemyColor;

    private ExtraConfigurationsService _extraConfigurationsService;
    private ITargetable _targetable;
    public void Configure(ITargetable target, bool isEnemy, ExtraConfigurationsService extraConfigurationsService)
    {
        _targetable = target;
        _extraConfigurationsService = extraConfigurationsService;

        UpdateValues(target.TargetInfo);
        SetColors(isEnemy);

    }

    private void UpdateValues(TargetInfo targetInfo)
    {
        SetNameText(targetInfo);

        SetTittleText(targetInfo);

        SetIcons(targetInfo);
    }

    private void SetIcons(TargetInfo targetInfo)
    {
        _rankImage.sprite = _extraConfigurationsService.RankIcons.GetRankSprite(targetInfo.Rank);
        _companyImage.sprite = _extraConfigurationsService.CompanyIcons.GetCompanySprite(targetInfo.Company);

        if (targetInfo.Rank == 0)
            _rankImage.enabled = false;
        if (targetInfo.Company == Company.Default)
            _companyImage.enabled = false;
    }

    private void SetNameText(TargetInfo targetInfo)
    {
        _nicknameText.SetText(targetInfo.Name);
    }

    private void SetTittleText(TargetInfo targetInfo)
    {
        var title = targetInfo.Title;
        if (title.Equals(string.Empty) == false)
            title = "<" + title + ">";
        _titleText.SetText(title);
    }

    private void SetColors(bool isEnemy)
    {
        var color = isEnemy ? _enemyColor : _allyColor;
        _nicknameText.color = color;
    }
}


