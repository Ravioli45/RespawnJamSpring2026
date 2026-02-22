using Godot;
using Godot.Collections;
using System;

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
    
	[Export]
	public int Hp = 2;

	[Export]
	CollisionShape2D EnemyCollider;
	[Export]
	Area2D ExplosionArea;
	[Export]
	private CollisionShape2D ExplosionCollider;
	[Export]
	private float BaseExplosionRadius;
	[Export]
	PackedScene ExplosionGraphic;

	public bool isInKnockback = false;
    public int hitlagtimer = 0;
	
	public bool isAttacking = false;
	public int ShootLag = 0;

	public bool isExploding = false;
	public int detonationtimer = 0;

	[Export]
	private PackedScene PlayerBullet;
	[Export] public AnimationTree animator;
	
    public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (isExploding)
		{
			detonationtimer--;
			if (detonationtimer == 20)
			{
				Explode(GameMaster.Instance.CurrentBuffs.ExplosionRadiusMultiplier, 10*GameMaster.Instance.CurrentBuffs.ExplosionDamageMultiplier);
				ExplodeGraphic Graphic = ExplosionGraphic.Instantiate<ExplodeGraphic>();
				GetParent().AddChild(Graphic);
				Graphic.GlobalPosition = GlobalPosition;
				Graphic.Explode();
			}
			else if (detonationtimer <= 0)
			{
				GD.Print("Die Now");
				CallDeferred("queue_free");
			}
		}

		else if (isInKnockback)
		{
			State = EnemyState.HITLAG;
			if (hitlagtimer <= 0)
			{
				isInKnockback = false;
				State = EnemyState.WALKING;
			}
			hitlagtimer--;
		}

		else if (isAttacking)
		{
			if (ShootLag <= 0)
			{
				isAttacking = false;
				State = EnemyState.WALKING;
			}
			ShootLag--;
			MoveAtPlayer();
			MoveAndSlide();

		}

		else
		{
			MoveAtPlayer();
			MoveAndSlide();
		}

		Godot.Vector2 direction = (GameMaster.Instance.PlayerPosition - this.GlobalPosition).Normalized();
		animator.Set("parameters/Walk/blend_position", direction * -1);
		animator.Set("parameters/Hit/blend_position", direction);
	}


    public void TakeDamage(int base_damage, Godot.Vector2 directionHit)
	{
		GD.Print("Ouch");
		AudioManager.Instance.PlaySFX("enemy_hit");
		this.Hp -= base_damage;
		if (this.Hp <= 0)
		{
			detonationtimer = 40;
			isExploding = true;
			EnemyCollider.Disabled = true;
			GameMaster.Instance.enemiesKilled += 1;
			State = EnemyState.DIE;
			//Explode(GameMaster.Instance.CurrentBuffs.ExplosionRadiusMultiplier, 10*GameMaster.Instance.CurrentBuffs.ExplosionDamageMultiplier);
			//Queue Free Enemy Here
			for (int i = 0; i < GameMaster.Instance.CurrentBuffs.ProjectilesOnDeath; i++)
			{
				Bullet newBullet = PlayerBullet.Instantiate<Bullet>();
				GetParent().AddChild(newBullet);
				newBullet.GlobalPosition = GlobalPosition;
				newBullet.Rotation = (float)GD.RandRange(0, 2 * Mathf.Pi);
				newBullet.Velocity = newBullet.GlobalTransform.X.Normalized() * 250;
			}
		}
		else
		{

			if (GD.Randf() < GameMaster.Instance.CurrentBuffs.ChainExplosionChance)
			{
				Explode(GameMaster.Instance.CurrentBuffs.ExplosionRadiusMultiplier, 10*GameMaster.Instance.CurrentBuffs.ExplosionDamageMultiplier);
				ExplodeGraphic Graphic = ExplosionGraphic.Instantiate<ExplodeGraphic>();
				GetParent().AddChild(Graphic);
				Graphic.GlobalPosition = GlobalPosition;
				Graphic.Explode();
			}
			//Knockback
			if (!isInKnockback)
			{
				isInKnockback = true;
				hitlagtimer = 30;
			}
			Velocity = directionHit * 1000;
			MoveAndSlide();
		}
	}
	public virtual void MoveAtPlayer()
	{
			Godot.Vector2 direction = (GameMaster.Instance.PlayerPosition - this.GlobalPosition).Normalized();
			Velocity = 100 * direction;
	}
	public void Explode(float explosionSizeMultiplier, float explosionDamage)
	{
		GD.Print("Explode!");
		

		//if (ExplosionGraphic is ExplodeGraphic e) e.Explode();
        //await ToSignal(GetTree().CreateTimer(.5), "timeout");

		
		AudioManager.Instance.PlaySFX($"explosion_{GD.RandRange(1, 6)}");
		(ExplosionCollider.Shape as CircleShape2D).Radius = BaseExplosionRadius * explosionSizeMultiplier;
		
		//GD.Print(ExplosionArea.GetOverlappingBodies());

		
		///*
		foreach(Node2D N in ExplosionArea.GetOverlappingBodies())
		{
			if (N is Enemy E)
			{
				if (E == this || E.Hp <= 0) continue;
				Godot.Vector2 dir = (E.GlobalPosition - this.Position).Normalized();

				E.TakeDamage((int)explosionDamage, dir);
			}
		}
		//*/
		
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