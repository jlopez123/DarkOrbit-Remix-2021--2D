using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipAnimConfig", menuName = "Create Ship Anim Configuration", order = 0)]
public class ShipAnimConfig : ScriptableObject
{
    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private float _fps;
    [SerializeField]
    private bool _animated = false;
    [SerializeField]
    private bool _flipX = false;

    public Sprite[] Sprites => _sprites;
    public float Fps => _fps;

    public bool Animated => _animated;
    public bool FlipX => _flipX;
}


