using Godot;
using System;

public partial class startaudioplayer : AudioStreamPlayer
{
	AudioStream startMusic;
	public override void _Ready()
	{
		startMusic = GD.Load<AudioStream>("res://assets/Audio/Start.wav");
		Stream = startMusic;
		Play();
	}

	
}
