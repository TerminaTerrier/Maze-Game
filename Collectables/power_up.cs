using Godot;
using System;

public partial class power_up : Node2D
{
    signalbus SignalBus;

    public override void _Ready()
    {
        SignalBus = GetNode<signalbus>("/root/Main/SignalBus");
    }

    public void OnArea2DBodyEntered(Node2D body)
	{
        if (body.Name == "Player")
        {
            GD.Print("Received");
            SignalBus.EmitSignal(signalbus.SignalName.ItemCollected, "power_up");
            QueueFree();
        }
    }
}
