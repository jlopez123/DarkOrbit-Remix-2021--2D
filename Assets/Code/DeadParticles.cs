using UnityEngine;

public class DeadParticles : MonoBehaviour
{
    [SerializeField]
    private PooledMonoBehaviour _deadParticles;

    private IHealth _entity;

    private void Awake()
    {
        _entity = GetComponent<IHealth>();
        _entity.OnDied += HandleDead;
    }
    private void HandleDead(IHealth obj)
    {
        if (_deadParticles == null)
            return;

        _deadParticles.Get<PooledMonoBehaviour>(transform.position, transform.rotation);
    }
}

public class ImpactParticles : MonoBehaviour
{
    /*
    
    [SerializeField]
    private PooledMonoBehaviour _impactParticles;

    private IHealth _entity;

    private void Awake()
    {
        _entity = GetComponent<IHealth>();
        _entity.OnHit += HandleHit;
    }
    private void OnDisable()
    {
        _entity.OnHit -= HandleHit;
    }
    private void HandleHit(bool onShield)
    {

    }
    */
}