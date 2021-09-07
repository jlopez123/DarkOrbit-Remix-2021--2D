using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    void LateUpdate()
    {
        if (_target == null)
            return;

        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }

    internal void SetTarget(Transform playerShip)
    {
        _target = playerShip;
    }
}
