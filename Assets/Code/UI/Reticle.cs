using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private FollowToTarget _followBehaviour;

    private ITargetable _currentTarget;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _followBehaviour = GetComponent<FollowToTarget>();
        QuitTarget();
    }
    public void SetTarget(ITargetable target)
    {
        _spriteRenderer.enabled = true;
        _currentTarget = target;
        _followBehaviour.SetTarget(target.gameObject.transform);
    }
    public void QuitTarget()
    {
        _currentTarget = null;
        _spriteRenderer.enabled = false;
    }
}
