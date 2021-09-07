using UnityEngine;

public class ProjectileFactory
{
    private readonly ProjectilesConfiguration _projectilesConfiguration;

    public ProjectileFactory(ProjectilesConfiguration projectilesConfiguration)
    {
        _projectilesConfiguration = projectilesConfiguration;
    }

    public Projectile Create(string projectileId, Vector3 position, Quaternion rotation, ProjectileConfig config, bool useSingleDamage = false)
    {
        Projectile prefab = _projectilesConfiguration.GetProjectileById(projectileId);
        var projectile = prefab.Get<Projectile>(position, rotation);

        projectile.Configure(config.Target, useSingleDamage ? projectile.SingleProjectileDamage : config.Damage , config.Direction, config.Owner);

        return projectile;
    }
}
