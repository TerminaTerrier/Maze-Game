using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using System.Linq;
using System.Reflection.Emit;


public partial class score_manager : Node
{
	[Signal]
	public delegate void HighScoreChangeEventHandler(int highScore);
	[Signal]
	public delegate void ScoreChangeEventHandler(int score);
	public static int score = 0;
	public static int highScore;
	signalbus SignalBus;
	[Export]
	Timer attackScoreTimer;


	public override void _Ready()
	{
		SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
		SignalBus.ItemCollected += OnItemCollected;
		SignalBus.EnemyDefeat += OnEnemyDefeat;
        attackScoreTimer.OneShot = true;
            
        
        if (!FileAccess.FileExists("user://savegame.save"))
		{
            GD.Print("no save found!");
            return;
			
		}
		else
		{
			LoadHighScore();
		}
	}

	public void LoadHighScore()
	{
		using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);

		var jsonString = saveGame.GetLine();

		var json = new Json();
		var parseResult = json.Parse(jsonString);

		var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);

		highScore = (int)nodeData["HighScore"];
		EmitSignal(SignalName.HighScoreChange, highScore);
		//GD.Print(highScore);
	}

	private void OnItemCollected(StringName collectable)
	{
		if (collectable == "pellet")
		{
			score += 10;
			HighScoreCheck();

		}
		if (collectable == "power_up")
		{
			score += 50;
			HighScoreCheck();
		}
		EmitSignal(SignalName.ScoreChange, score);
		//GD.Print(score);
	}

	private void OnEnemyDefeat()
	{
		int scoreMultiplier = 1;

		if(scoreMultiplier == 1)
		{
		attackScoreTimer.Start(5f);
		}
		
		score += GetAttackScore(scoreMultiplier);
		scoreMultiplier++;
		
		if(attackScoreTimer.IsStopped())
		{
			scoreMultiplier = 1;
		}
		EmitSignal(SignalName.ScoreChange, score);
		HighScoreCheck();
		GD.Print(score);
    }

    private int GetAttackScore(int multiplier)
	{
		return 250 * multiplier;
	}
	private void HighScoreCheck()
	{
		if (score > highScore)
		{
			highScore = score;
			EmitSignal(SignalName.HighScoreChange, highScore);
		}
	}

	public Godot.Collections.Dictionary<string, Variant> Save()
	{
		return new Godot.Collections.Dictionary<string, Variant>()
		{
			{ "HighScore", highScore }
		};

	}


}