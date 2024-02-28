using Godot;
using System;

public partial class game_manager : Node2D
{
	[Export]
	public Timer pauseTimer;
	float timerNum;
	[Signal]
	public delegate void SaveDataEventHandler();
	

	public override void _Ready()
	{
		
	}

	public void OnEnemyDefeat()
	{
		timerNum = 0.45f;
		pauseTimer.Start(timerNum);
		GetTree().Paused = true;
	}

	public void OnLifeLost()
	{
	   
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
