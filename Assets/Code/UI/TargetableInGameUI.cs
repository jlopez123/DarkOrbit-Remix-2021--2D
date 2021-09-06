using System;
using UnityEngine;

public abstract class TargetableInGameUI : MonoBehaviour
{

    protected UIHealthBar _healthBar;
    
    protected ITargetable _targetable;

    protected virtual void Awake()
    {
        _healthBar = GetComponentInChildren<UIHealthBar>();
    }

    public abstract void Disable();
}
