using Godot;
using System;



public partial class BuffIcon : Control
{
    [Export]
    TextureRect Icon;

    [Export]
    Label BuffName;

    [Export]
    Label Description;

    public BuffItemInfo cardBuff;

    public override void _Ready()
    {
        base._Ready();
        cardBuff = GameMaster.Instance.buffPool[GD.RandRange(0,6)];
        Icon.Texture = cardBuff.BuffTypeTexture;
        BuffName.Text = cardBuff.ItemName;
        Description.Text = cardBuff.Description;
    }

    public void OnButtonPressed(InputEvent @event)
    {
        if (@event.IsActionPressed("shoot")) {
            //GD.Print($"{BuffName.Text} Selected");
            GameMaster.Instance.CurrentBuffs += cardBuff.StatChanges;
        }
    }
}
