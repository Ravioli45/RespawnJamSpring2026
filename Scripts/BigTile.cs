using Godot;
using System;
using System.Collections.Generic;
//using Godot.Collections;

public partial class BigTile : Node2D
{

    [Export]
    public Vector2I Dimensions { get; set; }

    [Export]
    private TileMapLayer GroundLayer;
    public Vector2I GroundTileSize
    {
        get
        {
            return GroundLayer.TileSet.TileSize;
        }
    }
    private int GroundSourceId;
    private List<Vector2I> GroundLayerAtlasCoords;

    public override void _Ready()
    {
        base._Ready();
        GroundSourceId = GroundLayer.TileSet.GetSourceId(0);
        GroundLayerAtlasCoords = GetAllTileCoords(GroundLayer.TileSet);
        RandomizeBigTile();
    }

    public List<Vector2I> GetAllTileCoords(TileSet tileSet)
    {
        List<Vector2I> coords = [];
        TileSetAtlasSource atlasSource = tileSet.GetSource(tileSet.GetSourceId(0)) as TileSetAtlasSource;

        for (int i = 0; i < atlasSource.GetTilesCount(); i++)
        {
            coords.Add(atlasSource.GetTileId(i));
        }

        return coords;
    }

    public void RandomizeBigTile()
    {
        GroundLayer.Clear();
        for (int i = 0; i < Dimensions.X; i++)
        {
            for (int j = 0; j < Dimensions.Y; j++)
            {
                GroundLayer.SetCell(
                    new(i, j),
                    GroundSourceId,
                    GroundLayerAtlasCoords[GD.RandRange(0, GroundLayerAtlasCoords.Count - 1)]
                );
            }
        }
    }
}
