using Godot;
using System;

public partial class mainaudioplayer : AudioStreamPlayer
{	
	AudioStream gameMusic;
	signalbus Signalbus;
	public override void _Ready()
	{
		Signalbus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");

		gameMusic = GD.Load<AudioStream>("res://assets/Audio/Music.wav");
		Stream = gameMusic;
		Play();
	}

	

}
