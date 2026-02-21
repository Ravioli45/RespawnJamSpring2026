using Godot;
using System;

[GlobalClass]
public partial class BuffableStats : Resource
{
    [Export]
    public float FireRateMultiplier { get; set; }

    [Export]
    public float ProjectileDamageMultiplier { get; set; }

    [Export]
    public float ExplosionRadiusMultiplier { get; set; }

    [Export]
    public float ExplosionDamageMultiplier { get; set; }
    [Export]
    public float ChainExplosionChance { get; set; }

    [Export]
    public int ProjectilesOnDeath { get; set; }

    [Export]
    public float MoveSpeedMultiplier { get; set; }

    public BuffableStats() : this(0, 0, 0, 0, 0, 0, 0) { }

    public BuffableStats(
        float fireRateMultiplier,
        float projectileDamageMultiplier,
        float explosionRadiusMultiplier,
        float explosionDamageMultiplier,
        float chainExplosionChance,
        int projectilesOnDeath,
        float moveSpeedMultiplier
    )
    {
        FireRateMultiplier = fireRateMultiplier;
        ProjectileDamageMultiplier = projectileDamageMultiplier;
        ExplosionRadiusMultiplier = explosionRadiusMultiplier;
        ExplosionDamageMultiplier = explosionDamageMultiplier;
        ChainExplosionChance = chainExplosionChance;
        ProjectilesOnDeath = projectilesOnDeath;
        MoveSpeedMultiplier = moveSpeedMultiplier;
    }

    public static BuffableStats operator +(BuffableStats lhs, BuffableStats rhs)
    {
        lhs.FireRateMultiplier += rhs.FireRateMultiplier;
        lhs.ProjectileDamageMultiplier += rhs.ProjectileDamageMultiplier;
        lhs.ExplosionRadiusMultiplier += rhs.ExplosionRadiusMultiplier;
        lhs.ExplosionDamageMultiplier += rhs.ExplosionDamageMultiplier;
        lhs.ChainExplosionChance += rhs.ChainExplosionChance;
        lhs.ProjectilesOnDeath += rhs.ProjectilesOnDeath;
        lhs.MoveSpeedMultiplier += rhs.MoveSpeedMultiplier;

        return lhs;
    }
}
