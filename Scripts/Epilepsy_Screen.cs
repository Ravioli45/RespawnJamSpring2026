using Godot;
using System;

public partial class Epilepsy_Screen : Control
{

    public async override void _Ready()
    {
        base._Ready();
        Transition.Instance.Epilepsy();
        await ToSignal(GetTree().CreateTimer(7), "timeout");
        Transition.Instance.TransitionBetweenScenes();
        await ToSignal(GetTree().CreateTimer(1), "timeout");

        GetTree().ChangeSceneToFile("res://Scenes/TitleScreen.tscn");
    }

    public async override void _Process(double delta)
    {

        if (Input.IsAnythingPressed())
        {
            Transition.Instance.TransitionBetweenScenes();
            await ToSignal(GetTree().CreateTimer(1), "timeout");
            GetTree().ChangeSceneToFile("res://Scenes/TitleScreen.tscn");
        }
    }
}
