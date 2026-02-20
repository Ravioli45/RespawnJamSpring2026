using Godot;
using System;

public partial class Weapon : Node2D
{
    [Export]
    private Sprite2D WeaponSprite;
    [Export]
    private float BulletSpeed;
    [Export]
    private Node2D BulletSpawnPoint;
    [Export]
    private PackedScene BulletScene;

    public bool FlipV
    {
        get => WeaponSprite.FlipV;
        set => WeaponSprite.FlipV = value;
    }

    public void Shoot(Vector2? bias = null)
    {
        Vector2 bulletBias = bias ?? Vector2.Zero;

        Bullet bullet = BulletScene.Instantiate<Bullet>();
        GetParent().GetParent().AddChild(bullet);
        bullet.GlobalPosition = BulletSpawnPoint.GlobalPosition;
        bullet.Rotation = Rotation;
        bullet.Velocity = GlobalTransform.X.Normalized() * BulletSpeed + bulletBias;

        AudioManager.Instance.PlaySFX("player_shoot");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}
