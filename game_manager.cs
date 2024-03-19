using Godot;
using System;

public partial class game_manager : Node2D
{
	[Signal]
	public delegate void LoadSceneEventHandler();
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
		EmitSignal(SignalName.LoadScene);
	}

	public void OnEnemyDefeat()
	{
		timerNum = 0.45f;
		pauseTimer.Start(timerNum);
		GetTree().Paused = true;
	}

	public void OnLifeLost()
	{
	   timerNum = 1f;
	   pauseTimer.Start(timerNum);
	   GetTree().Paused = true;
	}

	public void OnLivesDepleted()
	{
		EmitSignal(SignalName.SaveData);
		GetTree().Paused = true;
	}

	public void _on_timer_timeout()
	{
		GetTree().Paused = false;
	}

	
}
