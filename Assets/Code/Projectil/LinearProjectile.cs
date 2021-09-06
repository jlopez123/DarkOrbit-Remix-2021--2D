using UnityEngine;

public class LinearProjectile : Projectile
{
    protected override void DoMove()
    {
        var angle = Utils.AngleBetweenVectors(_target.transform.position, transform.position);
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

        var step = _initialDistance * Time.deltaTime * _speed;
        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, step);
    }

    protected override void DoDestroy()
    {
        ReturnToPool(0f);
    }

    protected override void DoStart()
    {
    }
}
