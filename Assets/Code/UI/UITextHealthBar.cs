using TMPro;
using UnityEngine;

public class UITextHealthBar : UIHealthBar
{
    [SerializeField]
    private TextMeshProUGUI _hullText;
    [SerializeField]
    private TextMeshProUGUI _shieldText;
    protected override void HandleHealthChanged(float hullPct, float shieldPct)
    {
        _hullText.SetText(_health.Hull.ToString());
        _shieldText.SetText(_health.Shield.ToString());
    }
    protected override void HandleDead(IHealth obj)
    {

    }
}