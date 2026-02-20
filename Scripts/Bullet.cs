using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        MoveAndSlide();
    }

    public void OnScreenExit()
    {
        QueueFree();
    }
}
