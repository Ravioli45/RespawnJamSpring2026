using Godot;
using System;

public partial class Player : CharacterBody2D
{

    public enum PlayerState
    {
        IDLE,
        WALKING,
    }

    public PlayerState State { get; private set; } = PlayerState.IDLE;
    private bool canShoot = true;
    private bool invincible = false;

    [Export]
    private int Health;
    [Export]
    private float baseSpeed;
    [Export]
    private float baseFireRate;

    [Export]
    private Weapon Weapon;

    [Export]
    private AnimationTree animator;

    [Export]
    private Timer FirerateTimer;
    [Export]
    private Timer InvincibilityTimer;
    [Export]
    private Timer InvincibilityFlashTimer;

    public override void _Ready()
    {
        base._Ready();

        GameMaster.Instance.PlayerRef = this;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        Vector2 mouse_position = GetGlobalMousePosition();

        Weapon.Rotation = Mathf.Atan2(
            mouse_position.Y - Weapon.GlobalPosition.Y,
            mouse_position.X - Weapon.GlobalPosition.X
        );

        if (-Mathf.Pi / 2 <= Weapon.Rotation && Weapon.Rotation < Mathf.Pi / 2)
        {
            Weapon.FlipV = false;
        }
        else
        {
            Weapon.FlipV = true;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 direction = Input.GetVector("left", "right", "up", "down");

        if (Input.IsActionPressed("shoot") && canShoot)
        {
            Weapon.Shoot(Velocity);
            FirerateTimer.Start(baseFireRate / GameMaster.Instance.CurrentBuffs.FireRateMultiplier);
            canShoot = false;
        }

        if (!direction.IsZeroApprox())
        {
            State = PlayerState.WALKING;
        }
        else
        {
            State = PlayerState.IDLE;
        }

        if (!Mathf.IsZeroApprox(direction.X))
        {
            animator.Set("parameters/Walking/blend_position", direction.X);
            animator.Set("parameters/Idle/blend_position", direction.X);
        }

        Velocity = direction * baseSpeed * GameMaster.Instance.CurrentBuffs.MoveSpeedMultiplier;
        MoveAndSlide();
    }

    public void TakeHit()
    {
        if (!invincible)
        {
            Health -= 1;
            invincible = true;
            InvincibilityTimer.Start();
            InvincibilityFlashTimer.Start();
        }
    }

    public void OnFirerateTimerEnd()
    {
        canShoot = true;
    }

    public void OnInvincibilityEnd()
    {
        InvincibilityFlashTimer.Stop();
        Visible = true;
        invincible = false;
    }
    public void OnInvincibilityFlashEnd()
    {
        Visible = !Visible;
    }

    public bool IsIdle()
    {
        return State == PlayerState.IDLE;
    }

    public bool IsWalking()
    {
        return State == PlayerState.WALKING;
    }
}
