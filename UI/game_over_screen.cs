using Godot;
using System;

public partial class game_over_screen : Control
{
	[Signal]
	public delegate void RetryEventHandler();
	public override void _Ready()
	{
		
	}

	public void _on_button_pressed()
	{
		EmitSignal(SignalName.Retry);
	}
}
