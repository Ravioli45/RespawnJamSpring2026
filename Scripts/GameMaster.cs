using Godot;
using System;

public partial class GameMaster : Node
{
    public static GameMaster Instance { get; private set; }

    public Player PlayerRef { get; set; }

    [Export]
    public BuffableStats CurrentBuffs { get; set; }
    private BuffableStats InitialStats { get; set; }

    public Vector2 PlayerPosition
    {
        get => PlayerRef.GlobalPosition;
    }

    public override void _Ready()
    {
        base._Ready();

        if (Instance != null)
        {
            GD.PushWarning("More than one game master detected");
            return;
        }

        Instance ??= this;
        InitialStats = CurrentBuffs.Duplicate() as BuffableStats;
        GD.Print("GameMaster loaded");
    }
}
