using Godot;
using System;

public partial class livesdisplay : Label
{
	static int lives = 2;
	signalbus Signalbus;
	public override void _Ready()
	{
		Signalbus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
		Signalbus.LifeLost += OnLifeLost;
		Signalbus.LifeGain += OnLifeGain; 
		Signalbus.LivesDepleted += OnLivesDepleted;

		Text = "Lives: " + lives;
	}

	public void OnLifeLost()
	{
		
		if(lives >= 0)
		{
			lives--;
			Text = "Lives: " + lives;
			
		}
		
	}

	public void OnLifeGain()
	{
		if(lives <= 4)
		{
			lives++;
			Text = "Lives: " + lives;
		}
		
	}

	public void OnLivesDepleted()
	{
		lives = 2;
		Text = "Lives: " + lives;
	}

	//add an OnLifeGain signal when I implement that fuctionality
}
