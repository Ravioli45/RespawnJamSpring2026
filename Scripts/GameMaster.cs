using Godot;
using System;
using Godot.Collections;

public partial class GameMaster : Node
{
    public static GameMaster Instance { get; private set; }

    public Player PlayerRef { get; set; }
    public int enemiesKilled;
    public int wavesSurvived;

    [Export]
    public BuffableStats CurrentBuffs { get; set; }
    private BuffableStats InitialStats { get; set; }

    [Export]
    public Array<BuffItemInfo> buffPool;

    public Vector2 PlayerPosition
    {
        get
        {
            if (PlayerRef != null)
            {
                return PlayerRef.GlobalPosition;
            }
            else
            {
                return Vector2.Zero;
            }
        }
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

    public void Reset()
    {
        CurrentBuffs = InitialStats.Duplicate() as BuffableStats;
        enemiesKilled = 0;
        wavesSurvived = 0;
    }
}
