using UnityEngine;

public class CustomProjectile : Projectile
{
    [SerializeField]
    private Vector2 _rotateRandomValues;
    [SerializeField]
    private Vector2 _firstSpeed;
    [SerializeField]
    private AnimationCurve _curve;

    private float _forwardSpeed;
    private float _rotateSpeed;


    protected override void DoMove()
    {
        var angle = Utils.AngleBetweenVectors(_target.transform.position, transform.position);
        var rotationDesired = Quaternion.AngleAxis(-angle, Vector3.forward);

        var rotationStep = Time.deltaTime * _rotateSpeed;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationDesired, rotationStep);

        var angle2 = Quaternion.Angle(transform.rotation, rotationDesired);
        if (transform.rotation == rotationDesired)
        {
            var step = Time.deltaTime  * _speed;
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, step);
        }
        else
            MoveForward();
    }

    private void MoveForward()
    {
        transform.position += Time.deltaTime * -transform.right * _forwardSpeed;
    }

    protected override void DoDestroy()
    {
        ReturnToPool(0f);
    }

    protected override void DoStart()
    {
        _forwardSpeed = UnityEngine.Random.Range(_firstSpeed.x, _firstSpeed.y);
        _rotateSpeed = UnityEngine.Random.Range(_rotateRandomValues.x, _rotateRandomValues.y);
        _speed = UnityEngine.Random.Range(_speed, _speed + 2);
    }
}