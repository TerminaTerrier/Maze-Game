using Godot;
using System;

public partial class sceneloader : Node
{
	[Signal]
	 public delegate void MainLoadEventHandler();
	game_manager gameManager;
	Node Main;
	boot Boot;
	public override void _Ready()
	{
		Boot = GetNode<boot>("/root/boot");
		Boot.Boot += LoadMain;
		
		//can simplify these as get child
		gameManager = GetNode<game_manager>("/root/Sceneloader/Main");
		gameManager.LoadScene += ReloadMain;

		
		Main = GetNode<Node>("/root/Sceneloader/Main");
	}

	public void LoadMain()
	{
		var scene = GD.Load<PackedScene>("res://main.tscn");
		var mainInstance = (Node2D)scene.Instantiate();

		CallDeferred("add_child", mainInstance);
		EmitSignal(SignalName.MainLoad);
	}

	public void ReloadMain()
	{
		Main.QueueFree();

		LoadMain();
	}
}
