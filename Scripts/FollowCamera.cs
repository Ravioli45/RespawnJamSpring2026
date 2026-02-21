using Godot;
using System;

public partial class FollowCamera : Camera2D
{
    [Export]
    public Node2D Target { get; set; }
    [Export]
    public bool Following { get; set; }

    public override void _Process(double delta)
    {
        if (Following && IsInstanceValid(Target))
        {
            GlobalPosition = Target.GlobalPosition;
        }
        else
        {
            Target = null;
        }
    }
}
