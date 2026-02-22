using Godot;
using System;
using Godot.Collections;

public partial class Level : Node2D
{
	[Export]
	PackedScene SmallMelee;
	[Export]
	PackedScene LargeMelee;
	[Export]
	PackedScene SmallFlying;
	[Export]
	PackedScene LargeFlying;

	[Export]
	Node2D Spawnlocation;

	[Export]
	Player player;
	[Export]
	private Shop shop;
	[Export]
	private CanvasModulate LevelModulate;
	[Export]
	private Timer WaveTimer;
	[Export]
	public Array<PackedScene> EnemiesList;
	[Export]
	private PackedScene GameOverScene;
	private int WaveNumber = 1;
	//Difficulty increases spawwnrate
	private int difficulty = 1;

	private int spawntimer = 100;

	[Export]
	private Label InfoLabel;
	[Export]
	private Label TimerLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		AudioManager.Instance.PlayBGM("main");
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		InfoLabel.Text = $"HP: {player.Health}\n" +
		$"Enemies Killed: {GameMaster.Instance.enemiesKilled}\n" +
		$"Waves survived: {GameMaster.Instance.wavesSurvived}";

		TimerLabel.Text = $"Shop Timer: {(int)WaveTimer.TimeLeft}";
    }

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (spawntimer <= 0)
		{
			GD.Print("Spawning");

			float theta = (float)GD.RandRange(0, 6.28);
			Spawnlocation.GlobalPosition = new Vector2(player.GlobalPosition.X + 400 * MathF.Cos(theta), player.GlobalPosition.Y + 400 * MathF.Sin(theta));
			//Enemy NewEnemy = EnemiesList[0].Instantiate<Enemy>();
			Enemy NewEnemy = EnemiesList[GD.RandRange(0, EnemiesList.Count - 1)].Instantiate<Enemy>();
			AddChild(NewEnemy);
			NewEnemy.GlobalPosition = Spawnlocation.GlobalPosition;
			spawntimer = 100;
		}
		spawntimer--;

	}

	public void OnWaveTimerTimeout()
	{
		GD.Print("PAUSE");
		WaveNumber += 1;
		spawntimer -= difficulty;
		CheckEnemies(WaveNumber);
		GetTree().Paused = true;
		shop.Visible = true;
		LevelModulate.Color = new Color(.35f, .35f, .35f, 1);
		AudioManager.Instance.PlayBGM("shop");
		GameMaster.Instance.wavesSurvived += 1;
		InfoLabel.Visible = false;
		TimerLabel.Visible = false;
	}

	public void OnPlayerDied()
	{
		GameMaster.Instance.PlayerRef = null;
		GetTree().ChangeSceneToPacked(GameOverScene);
	}

	public void changeSpawnLocation()
	{
		//if on left somewhere go right
	}
	public void CheckEnemies(int roundNumber)
	{
		if (roundNumber == 3)
		{
			EnemiesList.Add(SmallFlying);
		}
		else if (roundNumber == 5)
		{
			EnemiesList.Add(LargeMelee);
		}
		else if (roundNumber == 7)
		{
			EnemiesList.Add(LargeFlying);
		}
	}

	public void OnShopClosed()
	{
		GetTree().Paused = false;
		WaveTimer.Start();
		LevelModulate.Color = new Color(1, 1, 1, 1);
		shop.Visible = false;
		AudioManager.Instance.PlayBGM("main");
		InfoLabel.Visible = true;
		TimerLabel.Visible = true;
	}
}
