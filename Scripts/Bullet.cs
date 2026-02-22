using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        MoveAndSlide();

        for (int i = 0; i < GetSlideCollisionCount(); i++)
            {
                var collision = GetSlideCollision(i);
                if (collision.GetCollider() is Enemy E)
                {
                    E.TakeDamage((int)(1 * GameMaster.Instance.CurrentBuffs.ProjectileDamageMultiplier), Velocity.Normalized());
					this.CallDeferred("queue_free");
                    break;
                }
            }

    }

    public void OnScreenExit()
    {
        QueueFree();
    }
}
