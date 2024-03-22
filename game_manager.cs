using Godot;
using System;

public partial class game_manager : Node2D
{
	[Signal]
	public delegate void LoadSceneEventHandler(string scene);
	[Signal]
	public delegate void GameOverEventHandler();
	[Export]
	public Timer pauseTimer;
	float timerNum;
	[Signal]
	public delegate void SaveDataEventHandler();
	

	public override void _Ready()
	{
		
	}

	public void OnLevelClear()
	{
		GetTree().Paused = true;
		EmitSignal(SignalName.LoadScene, "Main");
		GetTree().Paused = false;
	}

	public void OnEnemyDefeat()
	{
		timerNum = 0.45f;
		pauseTimer.Start(timerNum);
		GetTree().Paused = true;
	}

	public void OnLifeLost()
	{
		GD.Print("ouch!");
	     timerNum = 1f;
	    pauseTimer.Start(timerNum);
	    GetTree().Paused = true;
	}

	public void OnLivesDepleted()
	{
		GD.Print("game over man!");
		EmitSignal(SignalName.SaveData);
		
		GetTree().Paused = true;
		EmitSignal(SignalName.GameOver);
		GetTree().Paused = false;


	}

	public void _on_timer_timeout()
	{
		GetTree().Paused = false;
	}

	
}
