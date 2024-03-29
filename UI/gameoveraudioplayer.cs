using Godot;
using System;

public partial class gameoveraudioplayer : AudioStreamPlayer
{
	AudioStream gameOverMusic;
	public override void _Ready()
	{
		gameOverMusic = GD.Load<AudioStream>("res://assets/Audio/GameOver.wav");
		Stream = gameOverMusic;
		Play();
	}

}
