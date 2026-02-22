using Godot;
using System;
using System.Threading;

public partial class TitleScreen : Control
{
    [Export]
    TextureButton StartButton;

    [Export]
    TextureButton HelpButton;

    [Export]
    TextureButton ExitButton;

    [Export]
    TextureButton BackButton;

    [Export]
    TextureRect Logo;

    [Export]
    Label HelpDescription;

    

    public override void _Ready()
    {
        base._Ready();

    }

    public void Start()
    {
         GetTree().ChangeSceneToFile("res://Scenes/level.tscn");
    }

    public void Help()
    {
        StartButton.Visible = false;
        HelpButton.Visible = false;
        ExitButton.Visible = false;
        Logo.Visible = false;

        BackButton.Visible = true;
        HelpDescription.Visible = true;
    }

    public void Back()
    {
        BackButton.Visible = false;
        HelpDescription.Visible = false;

        StartButton.Visible = true;
        HelpButton.Visible = true;
        ExitButton.Visible = true;
        Logo.Visible = true;
    }

    public async void Exit()
    {
        Transition.Instance.TransitionBetweenScenes();
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        GetTree().Quit();
    }

}
