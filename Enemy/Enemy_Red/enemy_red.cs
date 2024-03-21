using Godot;
using System;

public partial class enemy_red : CharacterBody2D
{
	signalbus SignalBus;
	StateMachine stateMachine;
	public override void _Ready()
	{
		SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
        SignalBus.LifeLost += OnLifeLost;
		stateMachine = GetNode<StateMachine>("StateMachine");
	}

	public void OnLifeLost()
	{
		Vector2 spawn = new Vector2(601, 262);
        Position = spawn;
		stateMachine.TransitionTo("Idle");
	}
}
