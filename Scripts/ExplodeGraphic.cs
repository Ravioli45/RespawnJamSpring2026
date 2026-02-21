using Godot;
using System;

public partial class ExplodeGraphic : Node2D
{
    [Export]
    CpuParticles2D Explosion1;
    
    [Export]
    CpuParticles2D Explosion2;
    
    [Export]
    CpuParticles2D Explosion3;
    
    [Export]
    CpuParticles2D Explosion4;
    
    [Export]
    CpuParticles2D Explosion5;
    
    
    public override void _Process(double delta)
    {
        
    }

    public void Explode()
    {
        Explosion1.Emitting = true;
        Explosion2.Emitting = true;
        Explosion3.Emitting = true;
        Explosion4.Emitting = true;
        Explosion5.Emitting = true;
    }
}
