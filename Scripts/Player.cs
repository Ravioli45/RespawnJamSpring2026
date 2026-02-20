using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    private float speed;

    [Export]
    private Node2D Weapon;

    public override void _Process(double delta)
    {
        base._Process(delta);

        Vector2 mouse_position = GetGlobalMousePosition();

        Weapon.Rotation = Mathf.Atan2(
            mouse_position.Y - Weapon.GlobalPosition.Y,
            mouse_position.X - Weapon.GlobalPosition.X
        );
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 direction = Input.GetVector("left", "right", "up", "down");

        Velocity = direction * speed;

        MoveAndSlide();
    }
}
