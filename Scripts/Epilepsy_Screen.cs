using Godot;
using System;

public partial class Epilepsy_Screen : Control
{
    private bool InputLock;

    public async override void _Ready()
    {
        base._Ready();
        InputLock = true;

        Transition.Instance.Epilepsy();
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        InputLock = false;
        await ToSignal(GetTree().CreateTimer(6), "timeout");
        Transition.Instance.TransitionBetweenScenes();
        await ToSignal(GetTree().CreateTimer(1), "timeout");

        GetTree().ChangeSceneToFile("res://Scenes/TitleScreen.tscn");
    }

    public async override void _Process(double delta)
    {
        while (InputLock) return;

        if (Input.IsAnythingPressed())
        {
            InputLock = true;
            Transition.Instance.TransitionBetweenScenes();
            await ToSignal(GetTree().CreateTimer(1), "timeout");
            GetTree().ChangeSceneToFile("res://Scenes/TitleScreen.tscn");
        }
    }
}
