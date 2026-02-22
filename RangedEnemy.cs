using Godot;
using System;

public partial class RangedEnemy : Enemy
{	
	

	[Export]
    private PackedScene BulletScene;

	[Export]
    private Node2D BulletSpawnPoint;
	
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		if(ShootLag <= 0)
		{
			Shoot();
			MoveAtPlayer();
		}
    }


    public override void MoveAtPlayer()
    {
        Godot.Vector2 direction = (GameMaster.Instance.PlayerPosition - this.GlobalPosition).Normalized();
		if(ShootLag > 0)
		{
			Velocity = 30 * direction;
		}else{
		Velocity = 100 * direction;
		}
    }

	public void Shoot()
	{
		GD.Print("BANG");

		

        Bullet bullet = BulletScene.Instantiate<Bullet>();
        GetParent().AddChild(bullet);
        bullet.GlobalPosition = BulletSpawnPoint.GlobalPosition;
		Godot.Vector2 direction = (GameMaster.Instance.PlayerPosition - this.GlobalPosition).Normalized();
        bullet.Rotation = Mathf.Atan2(direction.Y, direction.X);
        bullet.Velocity = direction * 50;

		isAttacking  = true;
		ShootLag = 200;
	}
}
