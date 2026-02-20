using Godot;
using System;

public partial class Weapon : Node2D
{
    [Export]
    private Sprite2D WeaponSprite;
    [Export]
    private Node2D BulletSpawnPoint;

    public bool FlipV
    {
        get => WeaponSprite.FlipV;
        set => WeaponSprite.FlipV = value;
    }
}
