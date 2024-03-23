using Godot;
using System;

public partial class gameoveraudioplayer : AudioStreamPlayer
{
	AudioStream gameOverSFX;
	public override void _Ready()
	{
		gameOverSFX = GD.Load<AudioStream>("res://assets/Audio/GameOver.wav");
		Stream = gameOverSFX;
		Play();
	}

}
