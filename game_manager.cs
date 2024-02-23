using Godot;
using System;

public partial class game_manager : Node2D
{
    [Signal]
    public delegate void SaveDataEventHandler();
    

    public override void _Ready()
    {
        //healthComponent = GetNode<Node2D>("/root/Main/Player/HealthComponent");

        // Connect(signalbus.SignalName.LifeLost, Callable.From(OnLifeLost));
        //  Connect(signalbus.SignalName.LivesDepleted, Callable.From(OnLivesDepleted));
        
    }

    public void OnLifeLost()
    {
       
    }

    public void OnLivesDepleted()
	{
        EmitSignal(SignalName.SaveData);
        GetTree().Paused = true;
    }
}
