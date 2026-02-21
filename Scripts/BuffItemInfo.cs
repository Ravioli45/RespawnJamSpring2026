using Godot;
using System;

[GlobalClass]
public partial class BuffItemInfo : Resource
{
    [Export]
    public BuffableStats StatChanges { get; set; }

    [Export]
    public Texture2D BuffTypeTexture { get; set; }

    [Export]
    public string ItemName { get; set; }

    [Export]
    public string Description { get; set; }

    public BuffItemInfo() : this(null, null, null, null) { }

    public BuffItemInfo(BuffableStats statChanges, Texture2D buffTypeTexture, string itemName, string description)
    {
        StatChanges = statChanges;
        BuffTypeTexture = buffTypeTexture;
        ItemName = itemName;
        Description = description;
    }
}
