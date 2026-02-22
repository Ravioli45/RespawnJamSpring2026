using Godot;
using System;
using System.Numerics;

public partial class GameOver : Control
{
    [Export]
    Label StatsLabel;

    public override void _Ready()
    {
        base._Ready();
        AudioManager.Instance.PlayBGM("shop");
        StatsLabel.Text = "Great shootin', soldier!" + "\n"
                        + "Total waves survived: " + GameMaster.Instance.wavesSurvived + "\n"
                        + "Total enemies killed: " + GameMaster.Instance.enemiesKilled;
    }

    public async void MainMenu()
    {
        Transition.Instance.TransitionBetweenScenes();
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        GetTree().ChangeSceneToFile("res://Scenes/TitleScreen.tscn");
    }

    public async void Exit()
    {
        Transition.Instance.TransitionBetweenScenes();
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        GetTree().Quit();
    }

}
