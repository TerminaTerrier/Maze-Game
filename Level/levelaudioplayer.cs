using Godot;
using System;

public partial class levelaudioplayer : AudioStreamPlayer
{
	AudioStream collectSFX;
	AudioStream powerUpSFX;
	signalbus Signalbus;
	
	public override void _Ready()
	{
		Signalbus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
		Signalbus.ItemCollected += OnItemCollected;

		powerUpSFX = GD.Load<AudioStream>("res://assets/Audio/PowerUp.wav");
		collectSFX = GD.Load<AudioStream>("res://assets/Audio/Collect.wav");
	}

	public void OnItemCollected(StringName item)
	{
		if(item == "pellet")
		{
			Stream = collectSFX;
			Play();
		}

		if(item == "power_up")
		{
			Stream = powerUpSFX;
			Play();
		}
	}
}
