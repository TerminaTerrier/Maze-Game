using Godot;
using System;

public partial class playeraudioplayer : AudioStreamPlayer
{
	signalbus Signalbus;
	AudioStream deathSFX;
	public override void _Ready()
	{
		Signalbus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
		Signalbus.LifeLost += OnLifeLost;

		deathSFX = GD.Load<AudioStream>("res://assets/Audio/Death.wav");
	}

	public void OnLifeLost()
	{
		Stream = deathSFX;
		Play();
	}

}
