using Godot;
using System;

public partial class start_menu : Control
{
	[Signal]
	public delegate void GameStartEventHandler();
	public override void _Ready()
	{

	}

	public void _on_button_pressed()
	{
		EmitSignal(SignalName.GameStart);
	}
}
