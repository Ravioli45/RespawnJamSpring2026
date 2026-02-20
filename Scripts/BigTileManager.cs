using Godot;
using System;
using Godot.Collections;

public partial class BigTileManager : Node2D
{
    // Fired when a shift occurs. Passes in how much the grid was shifted by.
    [Signal]   
    public delegate void ShiftedEventHandler(Vector2 shift_amount);

    [Export]
    private PackedScene BigTileScene;

    [ExportSubgroup("Boundaries")]
    [Export]
    private float BoundaryBuffer;
    [Export]
    private Area2D LeftBoundary;
    [Export]
    private Area2D RightBoundary;
    [Export]
    private Area2D TopBoundary;
    [Export]
    private Area2D BottomBoundary;

    private readonly BigTile[,] BigTileMatrix = new BigTile[3, 3];

    private Vector2 Center = new(0, 0);
    public Vector2 BigTileSize { get; private set; }

    public override void _Ready()
    {
        base._Ready();

        for (int y = 0; y <= 2; y++)
        {
            for (int x = 0; x <= 2; x++)
            {

                BigTile tile = BigTileScene.Instantiate<BigTile>();

                BigTileSize = new Vector2(tile.GroundTileSize.X * tile.Dimensions.X, tile.GroundTileSize.Y * tile.Dimensions.Y) * tile.Scale;

                AddChild(tile);
                //tile.GlobalPosition = Center +
                //    new Vector2(BigTileSize.X * (x - 1),
                //    BigTileSize.Y * (y - 1)
                //);
                tile.GlobalPosition = Center + BigTileSize * new Vector2(x - 1, y - 1);

                //tile.RandomizeBigTile();

                BigTileMatrix[x, y] = tile;
            }
        }

        SetBoundaryPositions();
        //GD.Print("Big tile size: " + BigTileSize);
    }

    public void SetBoundaryPositions()
    {
        LeftBoundary.GlobalPosition = Center + new Vector2(-BoundaryBuffer, 0);

        TopBoundary.GlobalPosition = Center + new Vector2(0, -BoundaryBuffer);

        RightBoundary.GlobalPosition = Center + BigTileSize + new Vector2(BoundaryBuffer, 0);

        BottomBoundary.GlobalPosition = Center + BigTileSize + new Vector2(0, BoundaryBuffer);
        
    }

    public void ShiftLeft()
    {
        Center.X -= BigTileSize.X;

        BigTile[] temp = new BigTile[3];

        // clear out right
        for (int y = 0; y < 3; y++)
        {
            BigTile tile = BigTileMatrix[2, y];
            RemoveChild(tile);
            temp[y] = tile;
        }

        // shift remaining right in array
        for (int x = 1; x >= 0; x--)
        {
            for (int y = 0; y < 3; y++)
            {
                BigTileMatrix[x + 1, y] = BigTileMatrix[x, y];
            }
        }

        // put tiles back
        for (int y = 0; y < 3; y++)
        {
            BigTile tile = temp[y];
            AddChild(tile);
            tile.GlobalPosition = Center + BigTileSize * new Vector2(0 - 1, y - 1);
            tile.RandomizeBigTile();
            BigTileMatrix[0, y] = tile;
        }

        SetBoundaryPositions();
    }

    public void ShiftRight()
    {
        Center.X += BigTileSize.X;

        BigTile[] temp = new BigTile[3];

        // clear out left
        for (int y = 0; y < 3; y++)
        {
            BigTile tile = BigTileMatrix[0, y];
            RemoveChild(tile);
            temp[y] = tile;
        }

        // shift remaining left in array
        for (int x = 1; x <= 2; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                BigTileMatrix[x - 1, y] = BigTileMatrix[x, y];
            }
        }

        // put tiles back
        for (int y = 0; y < 3; y++)
        {
            BigTile tile = temp[y];
            AddChild(tile);
            tile.GlobalPosition = Center + BigTileSize * new Vector2(2 - 1, y - 1);
            tile.RandomizeBigTile();
            BigTileMatrix[2, y] = tile;
        }

        SetBoundaryPositions();
    }
    public void ShiftUp()
    {
        Center.Y -= BigTileSize.Y;
        
        BigTile[] temp = new BigTile[3];

        // clear out bottom
        for (int x = 0; x < 3; x++)
        {
            BigTile tile = BigTileMatrix[x, 2];
            RemoveChild(tile);
            temp[x] = tile;
        }

        // shift remaining down in matrix
        for (int y = 1; y >= 0; y--)
        {
            for (int x = 0; x < 3; x++)
            {
                //GD.Print(x + ":" + y);
                BigTileMatrix[x, y + 1] = BigTileMatrix[x, y];
            }
        }
        
        // put tiles back
        for (int x = 0; x < 3; x++)
        {
            BigTile tile = temp[x];
            AddChild(tile);
            tile.GlobalPosition = Center + BigTileSize * new Vector2(x - 1, 0 - 1);
            tile.RandomizeBigTile();
            BigTileMatrix[x, 0] = tile;
        }

        SetBoundaryPositions();
    }
    public void ShiftDown()
    {
        Center.Y += BigTileSize.Y;

        BigTile[] temp = new BigTile[3];

        // clear out top
        for (int x = 0; x < 3; x++)
        {
            BigTile tile = BigTileMatrix[x, 0];
            RemoveChild(tile);
            temp[x] = tile;
        }

        // shift remaining up in matrix
        for (int y = 1; y <= 2; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                //GD.Print(x + ":" + y);
                BigTileMatrix[x, y - 1] = BigTileMatrix[x, y];
            }
        }

        // put tiles back
        for (int x = 0; x < 3; x++)
        {
            BigTile tile = temp[x];
            AddChild(tile);
            tile.GlobalPosition = Center + BigTileSize * new Vector2(x - 1, 2 - 1);
            tile.RandomizeBigTile();
            BigTileMatrix[x, 2] = tile;
        }

        SetBoundaryPositions();
    }

    public void OnCrossLeft(Node2D _)
    {
        //GD.Print("Cross Left");
        ShiftLeft();
        EmitSignalShifted(new Vector2(-BigTileSize.X, 0));
    }
    public void OnCrossRight(Node2D _)
    {
        //GD.Print("Cross Right");
        ShiftRight();
        EmitSignalShifted(new Vector2(BigTileSize.X, 0));
    }
    public void OnCrossTop(Node2D _)
    {
        //GD.Print("Cross Top");
        ShiftUp();
        EmitSignalShifted(new Vector2(0, -BigTileSize.Y));
    }
    public void OnCrossBottom(Node2D _)
    {
        //GD.Print("Cross Bottom");
        ShiftDown();
        EmitSignalShifted(new Vector2(0, BigTileSize.Y));
    }
}
