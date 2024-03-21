using Godot;
using System;

public partial class enemy_green : CharacterBody2D
{
    [Export]
    public float gEnemySpeed { get; set;  } = 2f;
    StateMachine stateMachine;
    signalbus SignalBus;

    public override void _Ready()
    {
        SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");

        SignalBus.LifeLost += OnLifeLost;
        stateMachine = GetNode<StateMachine>("StateMachine");
        GD.Print(Position);
      
    }
    
    public void OnLifeLost()
    {
        Vector2 spawn = new Vector2(538, 262);
        Position = spawn;
        stateMachine.TransitionTo("Idle");
    }
}
