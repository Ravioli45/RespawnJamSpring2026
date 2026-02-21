using Godot;
using System;
using System.Numerics;

[GlobalClass]
public abstract partial class Enemy : CharacterBody2D
{
	public enum EnemyState
    {
        
        WALKING,
		HITLAG,
		DIE,
    }

	public EnemyState State { get; private set; } = EnemyState.WALKING;
    
    
	[Export] private AnimationTree animator;
	
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

    }


    public void TakeDamage(int base_damage, Godot.Vector2 directionHit)
	{
		GD.Print("Ouch");
	}
	public void MoveAtPlayer(Godot.Vector2 playerPosition)
	{
			Godot.Vector2 direction = (GameMaster.PlayerPosition - this.GlobalPosition).Normalized();
			Velocity = 20 * direction;
	}
	public void Explode(int explosionSizeMultiplier, int explosionDamage)
	{
		GD.Print("Explode!");
	}

	public bool IsWalking()
    {
        return State == EnemyState.WALKING;
    }

    public bool IsHit()
    {
        return State == EnemyState.HITLAG;
    }

	public bool IsDie()
    {
        return State == EnemyState.DIE;
    }
}