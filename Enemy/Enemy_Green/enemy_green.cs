using Godot;
using System;

public partial class enemy_green : CharacterBody2D
{
    [Export]
    public float gEnemySpeed { get; set;  } = 2f;

    signalbus SignalBus;

    public override void _Ready()
    {
        SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");

        SignalBus.LifeLost += OnLifeLost;

        GD.Print(Position);
    }

    public override void _PhysicsProcess(double delta)
    {
      
    


    }
    public override void _Process(double delta)
    {
       
    }
    
    public void OnLifeLost()
    {
        Vector2 spawn = new Vector2(538, 262);
        Position = spawn;
    }
}
