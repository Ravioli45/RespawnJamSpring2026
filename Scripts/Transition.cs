using Godot;
using System;

public partial class Transition : CanvasLayer
{

    public static Transition Instance {get; private set;}
    
    [Export]
    AnimationPlayer Fade;
    [Export]
    ColorRect Rect;

    public override void _Ready()
    {
        base._Ready();
        Instance ??= this;
        Rect.Visible = false;
    }

    public async void Epilepsy()
    {
        Rect.Visible = true;
        Fade.Play("Fade_To_Normal");
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        Rect.Visible = false;
    }

    public async void TransitionBetweenScenes()
    {
        Rect.Visible = true;
        Fade.Play("Fade_To_Black");
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        Fade.Play("Fade_To_Normal");
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        Rect.Visible = false;



    }
}
