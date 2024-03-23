using Godot;
using System;

public partial class enemyaudioplayer : AudioStreamPlayer
{
	AudioStream enemydefeatSFX;
	signalbus Signalbus;
	public override void _Ready()
	{
		Signalbus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
		Signalbus.EnemyDefeat += OnEnemyDefeat;

		enemydefeatSFX = GD.Load<AudioStream>("res://assets/Audio/EnemyDefeat.mp3");

	}

	public void OnEnemyDefeat()
	{
		Stream = enemydefeatSFX;
		Play();
	}
}
