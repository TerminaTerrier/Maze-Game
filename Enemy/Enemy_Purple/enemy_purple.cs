using Godot;
using System;

public partial class enemy_purple : CharacterBody2D
{
	signalbus SignalBus;
	
	public override void _Ready()
	{
		SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");

        SignalBus.LifeLost += OnLifeLost;
	}

	public void OnLifeLost()
	{
		Vector2 spawn = new Vector2(567, 214);
        Position = spawn;
	}
}
