using System;
using UnityEngine;
using UnityEngine.UI;

public class UIFillHealthBar : UIHealthBar
{
    [SerializeField]
    private Image _hullImage;
    [SerializeField]
    private Image _shieldImage;
    protected override void HandleHealthChanged(float hullPct, float shieldPct)
    {
        _hullImage.fillAmount = hullPct;
        _shieldImage.fillAmount = shieldPct;
    }

    protected override void HandleDead(IHealth obj)
    {
        obj.OnHealthChanged -= HandleHealthChanged;
        obj.OnDied -= HandleDead;

        _inGameUI.Disable();
    }
    private void OnDisable()
    {
        if (_health == null)
            return;

        _health.OnHealthChanged -= HandleHealthChanged;
        _health.OnDied -= HandleDead;
    }
}

public abstract class UIHealthBar : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;


    protected IHealth _health;
    protected TargetableInGameUI _inGameUI;
    public void Configure(IHealth health, TargetableInGameUI targetableInGameUI)
    {
        _health = health;
        _inGameUI = targetableInGameUI;

        _health.OnHealthChanged += HandleHealthChanged;
        _health.OnDied += HandleDead;

        //
        HandleHealthChanged(1f, 1f);
    }

    protected abstract void HandleDead(IHealth obj);

    protected abstract void HandleHealthChanged(float hullPct, float shieldPct);

    public virtual void SetVisible(bool visible)
    {
        _canvasGroup.SetVisible(visible);
    }
}
