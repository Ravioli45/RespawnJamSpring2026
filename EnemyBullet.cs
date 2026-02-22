using Godot;
using System;

public partial class EnemyBullet : Bullet
{

	[Export]
	AnimatedSprite2D sprite;
	public override void _Ready()
    {
		sprite.Play("default");
        
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        MoveAndSlide();

        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);
            if (collision.GetCollider() is Player P)
            {
                P.TakeHit();
                this.CallDeferred("queue_free");
                break;
            }
        }

    }

}
