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
        StatsLabel.Text = "Great shootin', soldier!" + "\n"
                        + "Total waves survived: " + "[Waves]" + "\n"
                        + "Total enemies killed: " + "[Kill Count]";
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
