using Godot;
using System;



public partial class BuffIcon : Control
{
    [Export]
    TextureRect Blaster;

    [Export]
    TextureRect Fist;

    [Export]
    TextureRect GiftBox;

     [Export]
    TextureRect Explosion;

    [Export]
    Label BuffName;

    [Export]
    Label Description;


    public override void _Ready()
    {
        base._Ready();

        int BuffID = GD.RandRange(1,4);
        if (BuffID == 1)
        {
            Blaster.Visible = true;
            BuffName.Text = "Buff 1";
            Description.Text = "This is buff number 1, it buffs you in a specific way";
        }

        else if (BuffID == 2)
        {
            Fist.Visible = true;
            BuffName.Text = "Buff 2";
            Description.Text = "This is buff number 2, it buffs you in a specific way";
        }

        else if (BuffID == 3)
        {
            GiftBox.Visible = true;
            BuffName.Text = "Buff 3";
            Description.Text = "This is buff number 3, it buffs you in a specific way";
        }

        else if (BuffID == 4)
        {
            Explosion.Visible = true;
            BuffName.Text = "Buff 4";
            Description.Text = "This is buff number 4, it buffs you in a specific way";
        }
    }

}
