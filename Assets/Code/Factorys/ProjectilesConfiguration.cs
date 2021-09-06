using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ProjectilesConfiguraton", menuName = "Factory/Create Projectiles Configuration", order = 0)]
public class ProjectilesConfiguration : ScriptableObject
{
    [SerializeField]
    private Projectile[] _projectilePrefabs;

    private Dictionary<string, Projectile> _idToProjectilePrefab = new Dictionary<string, Projectile>();

    private void Awake()
    {
        foreach (var projectile in _projectilePrefabs)
            _idToProjectilePrefab.Add(projectile.Id, projectile);
    }
    public Projectile GetProjectileById(string projectileId)
    {
        if(_idToProjectilePrefab.TryGetValue(projectileId, out var projectile))
            return projectile;

        throw new Exception($"Projectile {projectileId} not found");
    }
}