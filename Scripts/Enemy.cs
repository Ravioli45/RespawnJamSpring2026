using Godot;
using System;
using System.Net.Http;
using System.Numerics;

[GlobalClass]
public partial class Enemy : CharacterBody2D
{
	public enum EnemyState
    {
        
        WALKING,
		HITLAG,
		DIE,
    }

	public EnemyState State { get; private set; } = EnemyState.WALKING;
    
	public int Hp;

	public bool isInKnockback = false;
    public int hitlagtimer = 0;
	
	public bool isAttacking = false;
	public int ShootLag = 0;

	[Export] public AnimationTree animator;
	
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		if (isInKnockback)
		{
			State = EnemyState.HITLAG;
			if (hitlagtimer <= 0)
			{
				isInKnockback = false;
				State = EnemyState.WALKING;
			}
			hitlagtimer --;
		}else if(isAttacking){
			if(ShootLag <= 0)
			{
				isAttacking = false;
				State = EnemyState.WALKING;
			}
			ShootLag--;
			MoveAtPlayer();
			MoveAndSlide();
		}else{
			MoveAtPlayer();
			MoveAndSlide();
		}

		Godot.Vector2 direction = (GameMaster.Instance.PlayerPosition - this.GlobalPosition).Normalized();
        animator.Set("parameters/Walk/blend_position", direction *-1);
        animator.Set("parameters/Hit/blend_position", direction);
    }


    public void TakeDamage(int base_damage, Godot.Vector2 directionHit)
	{
		GD.Print("Ouch");
		//GameMaster.Instance.Stats.Hp -= base_damage;
		if(this.Hp <= 0)
		{
			//Explode(GameMaster.Instance.Stats.explosionSize, GameMaster.Instance.Stats.explosionDamage);
			//Queue Free Enemy Here
		}
		//Knockback
		if(!isInKnockback){
		isInKnockback = true;
		hitlagtimer = 30;
		}
		Velocity = directionHit * 1000;
		MoveAndSlide();
	}
	public virtual void MoveAtPlayer()
	{
			Godot.Vector2 direction = (GameMaster.Instance.PlayerPosition - this.GlobalPosition).Normalized();
			Velocity = 100 * direction;
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