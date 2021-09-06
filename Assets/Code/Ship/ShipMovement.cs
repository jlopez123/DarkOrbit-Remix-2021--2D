using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private float _speed;

    private IShip _myShip;

    public bool IsMoving { get; private set; }
    public float Speed => _speed;
    public void Configure(IShip ship, float speed)
    {
        _myShip = ship;
        _speed = speed;
    }
    public void Move(Vector2 targetPosition)
    {
        IsMoving = targetPosition != (Vector2)transform.position;

        var step = _speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    }
}
