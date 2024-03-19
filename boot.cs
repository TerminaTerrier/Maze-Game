using Godot;
using System;

public partial class boot : Node
{
	[Signal]
	public delegate void BootEventHandler();
	public override void _Ready()
	{
		EmitSignal(SignalName.Boot);
	}

}
