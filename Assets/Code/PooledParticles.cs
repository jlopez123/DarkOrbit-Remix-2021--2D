using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledParticles : PooledMonoBehaviour
{
    [SerializeField]
    private bool _backToPoolTransform = false;

    private void OnParticleSystemStopped()
    {
        if(_backToPoolTransform)
            BackToPoolTransform();

        gameObject.SetActive(false);
    }
}
