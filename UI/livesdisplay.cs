using Godot;
using System;

public partial class livesdisplay : Label
{
	int lives;
	signalbus Signalbus;
	public override void _Ready()
	{
		Signalbus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
		Signalbus.LifeLost += OnLifeLost;


		lives = 2;
		Text = "Lives: " + lives;
	}

	public void OnLifeLost()
	{
		lives--;
		Text = "Lives: " + lives;
		
	}

	//add an OnLifeGain signal when I implement that fuctionality
}
